using AcompanhamentoFisico.BLL;
using AcompanhamentoFisico.DAO;
using AcompanhamentoFisico.DTO;
using AcompanhamentoFisico.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcompanhamentoFisico.Controllers
{
	[Route("api/DadosPessoais")]
	[ApiController]
	public class DadosPessoaisController : ControllerBase
	{
		ClienteDAO dao = new ClienteDAO();
		ClienteBLL bll = new ClienteBLL();

		[HttpGet("{CPF}")]
		public CadastroPessoalDTO BuscaPorCPF(String CPF)
		{
			
			CadastroPessoalDTO cadastroPessoal = new CadastroPessoalDTO();

			cadastroPessoal = bll.retornaDadosPessoaisDoCliente(CPF);

			return cadastroPessoal;
		}


		
		[HttpPost]
		public String Post(CadastroPessoalDTO cadastroPessoal)
		{
			String retorno = bll.insereDadosPessoaisDoCliente(cadastroPessoal);

			return retorno;

		}

	
		[HttpPut]
		public String Put(CadastroPessoalDTO cadastroPessoal)
		{
			String retorno = bll.alteraDadosPessoais(cadastroPessoal);

			return retorno;
		}

		
		[HttpDelete("{CPF}")]
		public String Delete(String CPF)
		{

			String retorno = bll.deletaDadosPessoais(CPF);

			return retorno;
		}
	}
}
