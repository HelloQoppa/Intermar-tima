using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Intermaritima.UI.Web.Models;
using Intermaritima.ApplicationCore.Entity;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Intermaritima.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IConfiguration Configuration { get; }

        public IActionResult Index()
        {
            List<Estabelecimento> estabelecimentoList = new List<Estabelecimento>();

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SqlDataReader
                connection.Open();

                string sql = "Select * From Teacher";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Estabelecimento estabelecimento = new Estabelecimento();

                        estabelecimento.IdEstabelecimento = Convert.ToChar(dataReader["cod-estabel"]);
                        estabelecimento.ContaContabil = Convert.ToChar(dataReader["ct-codigo"]);
                        estabelecimento.CentroDeCusto = Convert.ToChar(dataReader["centro-custo"]);
                        estabelecimento.HistoricoDoLancamento = Convert.ToChar(dataReader["historico"]);
                        estabelecimento.DataEmissao = Convert.ToDateTime(dataReader["dt-emissao"]);
                        estabelecimento.Serie = Convert.ToChar(dataReader["serie-docto"]);
                        estabelecimento.Especie = Convert.ToChar(dataReader["cod-esp"]);
                        estabelecimento.Titulo = Convert.ToChar(dataReader["nro-docto"]);
                        estabelecimento.Cfop = Convert.ToChar(dataReader["nat-operacao"]);
                        estabelecimento.Emitente = Convert.ToInt32(dataReader["cod-emitente"]);
                        estabelecimento.Nome = Convert.ToChar(dataReader["nome-fornec"]);
                        estabelecimento.Requisicao = Convert.ToInt32(dataReader["nr-requisicao"]);
                        estabelecimento.Seq = Convert.ToInt32(dataReader["sequencia"]);
                        estabelecimento.NumNF = Convert.ToChar(dataReader["nr-nota"]);
                        estabelecimento.CodItem = Convert.ToChar(dataReader["it-codigo"]);
                        estabelecimento.ValorDebito = Convert.ToDecimal(dataReader["vl-debito"]);
                        estabelecimento.ValorCredito = Convert.ToDecimal(dataReader["vl-credito"]);
                        estabelecimento.Quantidade = Convert.ToDecimal(dataReader["qt-requisitada"]);
                    }
                }

                connection.Close();
            }
            return View(estabelecimentoList);
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [HttpPost]
        [ActionName("Create")]
        public IActionResult Create(Estabelecimento estabelecimento)
        {

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into es-ana-contab (cod-estabel,ct-codigo,centro-custo,historico,dt-emissao,serie-docto,cod-esp,nro-docto,nat-operacao,cod-emitente,nome-fornec,nr-requisicao,sequencia, nr-nota,it-codigo,vl-debito,vl-credito,qt-requisitada) Values ('{estabelecimento.IdEstabelecimento}','{estabelecimento.ContaContabil}','{estabelecimento.CentroDeCusto}','{estabelecimento.HistoricoDoLancamento}','{estabelecimento.DataEmissao}','{estabelecimento.Serie}','{estabelecimento.Especie}','{estabelecimento.Titulo}','{estabelecimento.Cfop}','{estabelecimento.Emitente}','{estabelecimento.Nome}','{estabelecimento.Requisicao}','{estabelecimento.Seq}','{estabelecimento.NumNF}','{estabelecimento.CodItem}','{estabelecimento.ValorDebito}','{estabelecimento.ValorCredito}','{estabelecimento.Quantidade}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Erro:" + ex.Message;
                        return RedirectToAction("Index");

                    }
                    connection.Close();
                }
                return View();
            }
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult Update(Estabelecimento estabelecimento)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update es-ana-contab SET ct-codigo='{estabelecimento.ContaContabil}', centro-custo='{estabelecimento.CentroDeCusto}',  historico='{estabelecimento.HistoricoDoLancamento}', dt-emissao='{estabelecimento.DataEmissao}',serie-docto='{estabelecimento.Serie}', cod-esp='{estabelecimento.Especie}', nro-docto='{estabelecimento.Titulo}', nat-operacao='{estabelecimento.Cfop}', cod-emitente='{estabelecimento.Emitente}', nome-fornec='{estabelecimento.Nome}', nr-requisicao='{estabelecimento.Requisicao}', sequencia='{estabelecimento.Seq}', nr-nota='{estabelecimento.NumNF}', it-codigo='{estabelecimento.CodItem}', vl-debito='{estabelecimento.ValorDebito}', vl-credito='{estabelecimento.ValorCredito}', qt-requisitada='{estabelecimento.Quantidade}' Where cod-estabel ='{estabelecimento.IdEstabelecimento}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Erro:" + ex.Message;
                    }
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From es-ana-contab Where Id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Erro:" + ex.Message;
                    }
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionName("Select")]
        public IActionResult Select(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From es-ana-contab Where Id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Erro:" + ex.Message;
                    }
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }
        public IActionResult ssa()
        {
            List<Estabelecimento> estabelecimentoList = new List<Estabelecimento>();

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select * From es-ana-contab";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Estabelecimento estabelecimento = new Estabelecimento();

                        estabelecimento.IdEstabelecimento = Convert.ToChar(dataReader["cod-estabel"]);
                        estabelecimento.ContaContabil = Convert.ToChar(dataReader["ct-codigo"]);
                        estabelecimento.CentroDeCusto = Convert.ToChar(dataReader["centro-custo"]);
                        estabelecimento.HistoricoDoLancamento = Convert.ToChar(dataReader["historico"]);
                        estabelecimento.DataEmissao = Convert.ToDateTime(dataReader["dt-emissao"]);
                        estabelecimento.Serie = Convert.ToChar(dataReader["serie-docto"]);
                        estabelecimento.Especie = Convert.ToChar(dataReader["cod-esp"]);
                        estabelecimento.Titulo = Convert.ToChar(dataReader["nro-docto"]);
                        estabelecimento.Cfop = Convert.ToChar(dataReader["nat-operacao"]);
                        estabelecimento.Emitente = Convert.ToInt32(dataReader["cod-emitente"]);
                        estabelecimento.Nome = Convert.ToChar(dataReader["nome-fornec"]);
                        estabelecimento.Requisicao = Convert.ToInt32(dataReader["nr-requisicao"]);
                        estabelecimento.Seq = Convert.ToInt32(dataReader["sequencia"]);
                        estabelecimento.NumNF = Convert.ToChar(dataReader["nr-nota"]);
                        estabelecimento.CodItem = Convert.ToChar(dataReader["it-codigo"]);
                        estabelecimento.ValorDebito = Convert.ToDecimal(dataReader["vl-debito"]);
                        estabelecimento.ValorCredito = Convert.ToDecimal(dataReader["vl-credito"]);
                        estabelecimento.Quantidade = Convert.ToDecimal(dataReader["qt-requisitada"]);

                        estabelecimentoList.Add(estabelecimento);
                    }
                }

                connection.Close();
            }
            return View(estabelecimentoList);
        }
    }
}
