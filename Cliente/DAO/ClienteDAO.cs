using AcompanhamentoFisico.DTO;
using AcompanhamentoFisico.Model;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AcompanhamentoFisico.DAO
{
	public class ClienteDAO
	{
		String conexao = @"Server=DESKTOP-BJNTUCI\MSSQLSERVER01;Database=Cliente;Trusted_Connection=True";

		#region Dados Pessoais
		public DadosPessoaisDTO retornaDadosPessoais(String CPF)
		{
			DadosPessoaisDTO dadosPessoaisDTO = new DadosPessoaisDTO();
			DadosPessoais dadosPessoais = new DadosPessoais();

			string sql = "select id_pk_cliente,nome,sexo,CPF,email,dataNascimento from dbo.DadosPessoaisCliente where CPF=" + CPF ;

			SqlConnection con = new SqlConnection(conexao);
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.CommandType = CommandType.Text;
			SqlDataReader reader;
			con.Open();

			try
			{
				reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					dadosPessoais.Id = Convert.ToInt32(reader[0].ToString());
					dadosPessoais.nome = reader[1].ToString();
					dadosPessoais.sexo = reader[2].ToString();
					dadosPessoais.CPF = reader[3].ToString();
					dadosPessoais.email = reader[4].ToString();
					dadosPessoais.dataNascimento = reader[5].ToString();
				   
					

					var configuration = new MapperConfiguration(cfg =>
					{
						cfg.CreateMap<DadosPessoais, DadosPessoaisDTO>();
					});
					var mapper = configuration.CreateMapper();
					dadosPessoaisDTO = mapper.Map<DadosPessoaisDTO>(dadosPessoais);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

			}
			finally
			{
				con.Close();
			}

			return dadosPessoaisDTO;
		}

		public int insereDadosPessoais(DadosPessoaisDTO dadosPessoaisDTO)
		{

			String retorno = "";
			string sql = "INSERT INTO dbo.DadosPessoaisCliente (nome,CPF,sexo,email,dataNascimento) VALUES (" + "'" + dadosPessoaisDTO.nome + "'" + "," +"'" + dadosPessoaisDTO.CPF + "'" +"," + "'" + dadosPessoaisDTO.sexo + "'" + "," + "'" + dadosPessoaisDTO.email + "'" + "," + "'" + dadosPessoaisDTO.dataNascimento + "'" + ")";
			SqlConnection con = new SqlConnection(conexao);
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.CommandType = CommandType.Text;
			SqlDataReader reader;
			con.Open();

			int i = cmd.ExecuteNonQuery();
		
			return i;
		}

		public int alteraDadosPessoais(DadosPessoaisDTO dadosPessoaisDTO)
		{

			String retorno = "";
			string sql = "UPDATE dbo.DadosPessoaisCliente SET  nome=" + "'" + dadosPessoaisDTO.nome+"'"+ ","+"sexo="+ "'" +dadosPessoaisDTO.sexo+"'" + "," + "dataNascimento="+ "'" + dadosPessoaisDTO.dataNascimento + "'" + "," + "email=" + "'" + dadosPessoaisDTO.email + "'" + "   where CPF="+ "'" + dadosPessoaisDTO.CPF + "'";
			SqlConnection con = new SqlConnection(conexao);
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.CommandType = CommandType.Text;
			SqlDataReader reader;
			con.Open();

			int i = cmd.ExecuteNonQuery();

			return i;
		}

		public int deletaDadosPessoais(String CPF)
		{
			String retorno = "";
			string sql = "delete from dbo.DadosPessoaisCliente where CPF = " + CPF;

			SqlConnection con = new SqlConnection(conexao);
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.CommandType = CommandType.Text;

			con.Open();

			int i = cmd.ExecuteNonQuery();
			return i;

		}

		#endregion


		#region Endereco
		public EnderecoClienteDTO retornaEndereco(String CPF)
		{
			EnderecoClienteDTO enderecoDTO = new EnderecoClienteDTO();
			EnderecoCliente endereco = new EnderecoCliente();

			string sql = "select endereco.rua,endereco.bairro, endereco.estado, endereco.numero, endereco.cidade, endereco.complemento, endereco.pais from dbo.EnderecoCliente endereco inner join dbo.DadosPessoaisCliente dadosPessoais on endereco.id_pk_cliente = dadosPessoais.id_pk_cliente where CPF =" + CPF;

			SqlConnection con = new SqlConnection(conexao);
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.CommandType = CommandType.Text;
			SqlDataReader reader;
			con.Open();

			try
			{
				reader = cmd.ExecuteReader();
				if (reader.Read())
				{

					endereco.rua = reader[0].ToString();
					endereco.bairro = reader[1].ToString();
					endereco.estado = reader[2].ToString();
					endereco.numero = Convert.ToInt32(reader[3]);
					endereco.cidade = reader[4].ToString();
					endereco.complemento = reader[5].ToString();
					endereco.pais = reader[6].ToString();

					var configuration = new MapperConfiguration(cfg =>
					{
						cfg.CreateMap<EnderecoCliente, EnderecoClienteDTO>();
					});
					var mapper = configuration.CreateMapper();
					enderecoDTO = mapper.Map<EnderecoClienteDTO>(endereco);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

			}
			finally
			{
				con.Close();
			}

			return enderecoDTO;
		}

		public int insereEndereco(EnderecoClienteDTO endereco,String CPF)
		{

			String retorno = "";
			string sql = "INSERT INTO dbo.EnderecoCliente (rua,bairro,cidade,estado,numero,pais,complemento,id_pk_cliente) VALUES (" + "'" + endereco.rua + "'" + "," +"'" + endereco.bairro + "'" + "," +"'"+endereco.cidade+"'"+","+ "'" + endereco.estado+"'"+","+ endereco.numero + ","+ "'" + endereco.pais + "'" + ","  + "'" + endereco.complemento + "'" +"," + "(select id_pk_cliente from dbo.DadosPessoaisCliente where CPF=" + CPF.ToString()+")"+" )";
			SqlConnection con = new SqlConnection(conexao);
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.CommandType = CommandType.Text;
			SqlDataReader reader;
			con.Open();

			int i = cmd.ExecuteNonQuery();
		
			return i;
		}

		public int alteraEndereco(EnderecoClienteDTO endereco,String CPF)
		{

			String retorno = "";
			string sql = "update dbo.EnderecoCliente set rua="+ "'" + endereco.rua + "'" + ","+ "bairro=" +"'" + endereco.bairro + "'" + "," + "cidade=" +"'" + endereco.cidade + "'" + "," +"estado=" + "'" + endereco.estado + "'" + "," +"numero="+ endereco.numero + "," + "complemento=" + "'" + endereco.complemento + "'" + "," + "pais=" + "'" +  endereco.pais + "'" +  "  where id_pk_cliente = (select id_pk_cliente from dbo.DadosPessoaisCliente where CPF=" + "'" + CPF +"'" +");";
			SqlConnection con = new SqlConnection(conexao);
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.CommandType = CommandType.Text;
			SqlDataReader reader;
			con.Open();

			int i = cmd.ExecuteNonQuery();

			return i;
		}

		public int deletaEndereco(String CPF)
		{
			String retorno = "";
			string sql = "delete from dbo.EnderecoCliente where  id_pk_cliente = (select id_pk_cliente from dbo.DadosPessoaisCliente where CPF=" + CPF +");";

			SqlConnection con = new SqlConnection(conexao);
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.CommandType = CommandType.Text;

			con.Open();

			int i = cmd.ExecuteNonQuery();
			return i;

		}

        #endregion


        #region Telefone

        public int alteraTelefone(TelefoneClienteDTO telefone, String CPF)
        {

            String retorno = "";
            string sql = "update dbo.TelefoneCliente set tipo_telefone=" + "'" + telefone.tipoTelefone + "'" + "," + "numero=" + telefone.numero + "  where id_pk_cliente = (select id_pk_cliente from dbo.DadosPessoaisCliente where CPF=" + "'" + CPF + "'" + ");";
            SqlConnection con = new SqlConnection(conexao);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();

            int i = cmd.ExecuteNonQuery();

            return i;
        }



        public TelefoneClienteDTO retornaTelefone(String CPF)
        {
            TelefoneClienteDTO telefoneDto = new TelefoneClienteDTO();
            TelefoneCliente telefone = new TelefoneCliente();

            string sql = "select numero, tipo_telefone  from TelefoneCliente as telefone inner join dbo.DadosPessoaisCliente dadosPessoais on telefone.id_pk_cliente = dadosPessoais.id_pk_cliente where CPF =" + CPF;

            SqlConnection con = new SqlConnection(conexao);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();

            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {

					telefone.numero = Convert.ToInt64(reader[0]);
                    telefone.tipoTelefone = reader[1].ToString();

                    var configuration = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<TelefoneCliente, TelefoneClienteDTO>();
                    });
                    var mapper = configuration.CreateMapper();
                    telefoneDto = mapper.Map<TelefoneClienteDTO>(telefone);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                con.Close();
            }

            return telefoneDto;
        }
        public int insereTelefone(TelefoneClienteDTO telefone, String CPF)
        {

            String retorno = "";
            string sql = "INSERT INTO dbo.TelefoneCliente (numero, tipo_telefone,id_pk_cliente) VALUES (" + telefone.numero  + "," + "'" + telefone.tipoTelefone + "'" + "," + "(select id_pk_cliente from dbo.DadosPessoaisCliente where CPF=" + CPF.ToString() + ")" + " )";
            SqlConnection con = new SqlConnection(conexao);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();

            int i = cmd.ExecuteNonQuery();

            return i;
        }

        public int deletaTelefoneCliente(String CPF)
        {
            String retorno = "";
            string sql = "delete from dbo.TelefoneCliente where  id_pk_cliente = (select id_pk_cliente from dbo.DadosPessoaisCliente where CPF=" + CPF + ");";

            SqlConnection con = new SqlConnection(conexao);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            con.Open();

            int i = cmd.ExecuteNonQuery();
            return i;

        }
        #endregion
    }
}
