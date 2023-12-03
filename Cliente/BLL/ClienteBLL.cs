using AcompanhamentoFisico.DAO;
using AcompanhamentoFisico.DTO;
using AcompanhamentoFisico.Model;
using System.Linq.Expressions;
using System;

namespace AcompanhamentoFisico.BLL
{
	public class ClienteBLL
	{
		ClienteDAO dao = new ClienteDAO();
		String retorno = "";
		int retornoDadosPessoais = 0;
		int retornoEndereco = 0;
		int retornoTelefone = 0;


        public CadastroPessoalDTO retornaDadosPessoaisDoCliente(String CPF)
		{
			
			CadastroPessoalDTO cadastroPessoal = new CadastroPessoalDTO();

			cadastroPessoal.dadosPessoais = dao.retornaDadosPessoais(CPF);
			cadastroPessoal.endereco = dao.retornaEndereco(CPF);
			cadastroPessoal.telefone = dao.retornaTelefone(CPF);


			return cadastroPessoal;
		}

		public String insereDadosPessoaisDoCliente(CadastroPessoalDTO cadastroPessoal)
		{
			retorno = "";
			retornoDadosPessoais = 0;
			retornoEndereco = 0;
			retornoTelefone = 0;

			bool retornoCPFValidacao = ValidaCPF(cadastroPessoal.dadosPessoais.CPF);
			bool retornoNomeValidacao = validaNome(cadastroPessoal.dadosPessoais.nome);
			bool retornoTelefoneValidacao = validaTelefone(cadastroPessoal.telefone);
			bool retornoVerificaDadosObrigatorios = validaCamposObrigatorios(cadastroPessoal);


            if (retornoCPFValidacao	 && 
				retornoNomeValidacao && 
				retornoTelefoneValidacao &&
                retornoVerificaDadosObrigatorios
                )
			{
				if (cadastroPessoal.dadosPessoais != null)
				{
					retornoDadosPessoais = dao.insereDadosPessoais(cadastroPessoal.dadosPessoais);
				}
				if (cadastroPessoal.endereco != null)
				{
					retornoEndereco = dao.insereEndereco(cadastroPessoal.endereco, cadastroPessoal.dadosPessoais.CPF);
				}

				if (cadastroPessoal.telefone != null)
				{
					retornoTelefone = dao.insereTelefone(cadastroPessoal.telefone, cadastroPessoal.dadosPessoais.CPF);
				}

				if (retornoDadosPessoais == 1 && retornoEndereco == 1 && retornoTelefone == 1)
				{
					retorno = "Cadastro realizado com sucesso";
				}
				else if (retornoDadosPessoais == 0)
				{
					retorno = "Não foi possível cadastrar dados pessoais";
				}
				else if (retornoEndereco == 0)
				{
					retorno = "Não foi possível cadastrar endereço";
				}
				else if (retornoTelefone == 0)
				{
					retorno = "Não foi possível cadastrar telefone";
				}

			}
			
			if(!retornoCPFValidacao)
			{
				retorno = "CPF inválido";
			}

			if(!retornoNomeValidacao)
			{
				retorno = "Nome sem o mínimo de caracteres preencha o nome e o sobrenome";
			}
			
			if(!retornoTelefoneValidacao)
			{
				retorno = "Telefone inválido";
			}
			
			if(!retornoVerificaDadosObrigatorios)
			{
				retorno = "Campo obrigatório não preenchido";
			}

			return retorno;
		}

		public String alteraDadosPessoais(CadastroPessoalDTO cadastroPessoal)
		{
			retorno = "";
			retornoDadosPessoais = 0;
			retornoEndereco = 0;
			retornoTelefone = 0;

            retornoDadosPessoais = dao.alteraDadosPessoais(cadastroPessoal.dadosPessoais);
            retornoEndereco = dao.alteraEndereco(cadastroPessoal.endereco, cadastroPessoal.dadosPessoais.CPF);
			retornoTelefone = dao.alteraTelefone(cadastroPessoal.telefone, cadastroPessoal.dadosPessoais.CPF);
			

			if (retornoDadosPessoais == 1 && retornoEndereco == 1 && retornoTelefone ==1)
			{
				retorno = "Alteração realizada com sucesso";
			}
			else if (retornoDadosPessoais == 0)
			{
				retorno = "Não foi possível alterar dados pessoais";
			}
			else if (retornoEndereco == 0)
			{
				retorno = "Não foi possível alterar endereço";
			}



			return retorno;
		}

		public String deletaDadosPessoais(String CPF)
		{
			retorno = "";
			retornoDadosPessoais = 0;
			retornoEndereco = 0;
			retornoTelefone = 0;

			if(CPF!=null) 
			{
                retornoTelefone = dao.deletaTelefoneCliente(CPF);
                retornoEndereco = dao.deletaEndereco(CPF);
			    retornoDadosPessoais = dao.deletaDadosPessoais(CPF);
			

            }
            if (retornoDadosPessoais == 1 && retornoEndereco == 1)
			{
				retorno = "Dados deletados com sucesso";
			}
			else if (retornoDadosPessoais == 0)
			{
				retorno = "Não foi possível deletar dados pessoais";
			}
			else if (retornoEndereco == 0)
			{
				retorno = "Não foi possível deletar endereço";
			}
			else if(retornoTelefone==0)
			{
				retorno = "Não foi possível deletar telefone";
			}



			return retorno;
		}


# region Valida CPF
        public static bool ValidaCPF(string cpf)
        {
            return (IsCpf(cpf));
        }

        private static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
        #endregion

#region Valida nome

		private static bool validaNome(String nome)
		{


			return (IsNome(nome));
		}

		private static bool IsNome(String nome)
        {

			return nome.Length > 6 ? true : false;
        }
        #endregion

#region ValidaTelefone

        private static bool validaTelefone(TelefoneClienteDTO telefone)
        {


            return (IsTelefone(telefone));
        }

        private static bool IsTelefone(TelefoneClienteDTO telefone)
        {
			bool retornoTelefone=false;

			if(telefone.tipoTelefone=="Celular")
			{
			  	   retornoTelefone = telefone.numero.ToString().Length == 11;
			}
			else if(telefone.tipoTelefone=="Residencial" || telefone.tipoTelefone=="Comercial")
			{
                retornoTelefone = telefone.numero.ToString().Length == 10;

            }
			return retornoTelefone;
            
        }

        #endregion

#region Dados obrigatórios
        private static bool validaCamposObrigatorios(CadastroPessoalDTO cadastroPessoal)
        {
			return (
				   (cadastroPessoal.dadosPessoais.nome != null
				   || cadastroPessoal.dadosPessoais.nome != "" ? true : false)
				   && (cadastroPessoal.telefone!=null ? true : false)
				   && (cadastroPessoal.endereco.rua!=null || cadastroPessoal.endereco.rua!="" ? true: false)
				   && (cadastroPessoal.endereco.cidade!=null || cadastroPessoal.endereco.cidade!="" ? true : false)
				   && (cadastroPessoal.endereco.pais!=null || cadastroPessoal.endereco.pais!="" ? true : false)
				   && (cadastroPessoal.endereco.estado!=null || cadastroPessoal.endereco.estado!=""));

			

        }


        #endregion

    }
}
