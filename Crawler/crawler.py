import requests
import requests
import json
from bs4 import BeautifulSoup
from typing import List

url = "http://esaj.tjba.jus.br/cpo/sg/search.do?paginaConsulta=1&cbPesquisa=NUMPROC&tipoNuProcesso=UNIFICADO&numeroDigitoAnoUnificado=%s&foroNumeroUnificado=&dePesquisaNuUnificado=%s&dePesquisa="

class Movimentacao:
    def __init__(self, data, descricao, possui_anexo, legenda):
        self.data = data
        self.descricao = descricao
        self.possuianexo = possui_anexo
        self.legenda = legenda

class Processo:
    def __init__(self, numero, classe, area, assunto, origem, distribuicao, relator, movimentacoes):
        self.numero = numero
        self.classe = classe
        self.area = area
        self.assunto = assunto
        self.origem = origem
        self.distribuicao = distribuicao
        self.relator = relator        
        self.movimentacoes = movimentacoes

numero_do_processo = "0809979-67.2015.8.05.0080"
partes_numero_processo = numero_do_processo.split(".")
sequencial_processo = partes_numero_processo[0]
ano_processo = partes_numero_processo[1]

sequencia_ano_processo = sequencial_processo + ano_processo
url = url % (sequencia_ano_processo, numero_do_processo)

page = requests.get(url)
soup = BeautifulSoup(page.text, 'html.parser')

bloco_dados_processo = soup.find_all(class_= "secaoFormBody")[1]
campos_processo = bloco_dados_processo.find_all("tr")

numero_processo = campos_processo[1].text.strip()
dados_classe = campos_processo[3].text.strip()
dados_area = campos_processo[5].text.strip().split(": ")[1]
dados_assunto = campos_processo[6].text.strip().split(":")[1].strip()
dados_origem = campos_processo[7].text.strip().split(":")[1].strip()
dados_distribuicao = campos_processo[9].text.strip().split(":")[1].strip()
dados_relator = campos_processo[10].text.strip().split(":")[1].strip()

bloco_movimentacoes = soup.find("table", {"id": "tabelaTodasMovimentacoes"})
linhas_movimentacoes = bloco_movimentacoes.find_all("tr")

lista_movimentacoes = []

for linha_movimentacao in linhas_movimentacoes:
    data_movimentacao = linha_movimentacao.find_all("td")[0].text.strip()
    descricao_movimentacao = linha_movimentacao.find_all("td")[2].next.strip()
    possui_anexo = not linha_movimentacao.find_all("td")[1].find(class_="linkMovVincProc") is None
    legenda_movimentacao = linha_movimentacao.find_all("td")[2].find("span").text.strip()

    movimentacao = Movimentacao(data_movimentacao, descricao_movimentacao, possui_anexo, legenda_movimentacao)
    lista_movimentacoes.append(movimentacao)

processo = Processo(numero_processo, dados_classe, dados_area, dados_assunto, dados_origem, dados_distribuicao, dados_relator, lista_movimentacoes)

URL_POST_PROCESSO = "http://localhost:50207/api/Processos"
headers = {'Content-Type': 'application/json', 'Accept':'application/json'}
r = requests.post(URL_POST_PROCESSO, json.dumps(processo, default= lambda o: o.__dict__), headers = headers)


