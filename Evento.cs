using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Sistema_de_Gerenciamento_de_Conferências
{
    public class Evento
    {
        [DisplayName("Atividade")]
        public string NomeDaPalestra { get; set; } = "";

        [DisplayName("Duração"), Description("Duração da atividade em minutos.")]
        public int TempoDaPalestra { get; set; }

        [DisplayName("Horário inicial"), JsonIgnore]
        public TimeOnly? Horario { get; set; } = null;

        [DisplayName("Horário final"), JsonIgnore]
        public TimeOnly? HorarioFinal
        {
            get
            {
                if (!Horario.HasValue)
                {
                    return null;
                }

                return Horario.Value.AddMinutes(TempoDaPalestra);
            }
        }

        [DisplayName("Atividade relâmpago"), JsonIgnore]
        public bool isRelampago
        {
            get
            {
                return TempoDaPalestra <= 5;
            }
        }
    }
}