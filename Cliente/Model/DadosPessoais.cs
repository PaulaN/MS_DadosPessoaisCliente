using System.Runtime.CompilerServices;

namespace AcompanhamentoFisico.Model
{
	public class DadosPessoais
	{
		public int Id { get; set; }
		public String nome {get;set;}
		public String sexo {get;set;}
		public String CPF {get; set;}
		public String dataNascimento { get; set; }

        public String email { get; set; }
        public int IdProfissionais { get; set; }

		public int IdMedidas { get; set; }

	}
}
