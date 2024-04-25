using System.ComponentModel;
using System.Text.Json;

namespace Sistema_de_Gerenciamento_de_Conferências
{
    public partial class Form1 : Form
    {
        readonly BindingList<Evento> Trilha1 = [];
        readonly BindingList<Evento> Trilha2 = [];
        readonly List<Evento> PalestrasDescartadas = [];

        public Form1()
        {
            InitializeComponent();
            trilha1Grid.DataSource = Trilha1;
            trilha2Grid.DataSource = Trilha2;
        }

        private void ImportarPalestraJSON(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "Arquivo JSON (*.json)|*.json"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                List<Evento>? eventos;

                try
                {
                    eventos = JsonSerializer.Deserialize<List<Evento>>(File.ReadAllText(filePath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao ler arquivo: {ex.Message}");
                    return;
                }

                if (eventos == null)
                {
                    MessageBox.Show("Erro ao ler arquivo.");
                    return;
                }

                try
                {
                    OrganizaTrilhas(eventos);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro processando trilhas: {ex.Message}");
                }
            }
        }

        private void AdiconarPalestra(object sender, EventArgs e)
        {
            List<Evento> todosEventos = ListaCompleta();

            Evento? eventoExistente = todosEventos.FirstOrDefault(x => x.NomeDaPalestra.Equals(PalestraNome.Text, StringComparison.CurrentCultureIgnoreCase));

            if (eventoExistente != null)
            {
                MessageBox.Show("Palestras devem ter nomes diferentes");
                return;
            }

            if (!int.TryParse(PalestraTempo.Text, out int palestraTempo))
            {
                MessageBox.Show("Duração deve ser numérica");
                return;
            }

            if (palestraTempo < 2 || palestraTempo > 90)
            {
                MessageBox.Show("Palestras devem ser entre 2 e 90 minutos");
                return;
            }

            todosEventos.ForEach(x => x.Horario = null);
            todosEventos.RemoveAll(x => x.NomeDaPalestra == "Almoço");
            todosEventos.RemoveAll(x => x.NomeDaPalestra == "Networking");

            Evento palestra = new()
            {
                NomeDaPalestra = PalestraNome.Text,
                TempoDaPalestra = palestraTempo
            };

            todosEventos.Add(palestra);

            OrganizaTrilhas(todosEventos);
        }

        private List<Evento> ListaCompleta()
        {
            List<Evento> todosEventos = [];

            todosEventos.AddRange([.. Trilha1]);
            todosEventos.AddRange([.. Trilha2]);
            todosEventos.AddRange([.. PalestrasDescartadas]);

            return todosEventos;
        }

        private void SalvarJson(object sender, EventArgs e)
        {
            List<Evento> todosEventos = ListaCompleta();

            todosEventos.RemoveAll(x => x.NomeDaPalestra == "Almoço");
            todosEventos.RemoveAll(x => x.NomeDaPalestra == "Networking");

            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            string listaJson = JsonSerializer.Serialize(todosEventos, options);

            SaveFileDialog openFileDialog = new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "Arquivo JSON (*.json)|*.json"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                File.WriteAllText(filePath, listaJson);
            }
        }

        private void OrganizaTrilhas(List<Evento> eventos)
        {
            List<Evento> trilha1 = [.. eventos.OrderBy(p => p.TempoDaPalestra)];
            DistribuirHorarios(trilha1, true);

            Trilha1.Clear();
            trilha1 = [.. trilha1.OrderBy(x => x.Horario)];

            foreach (Evento evento in trilha1)
            {
                if (evento.Horario == null)
                {
                    continue;
                }

                Trilha1.Add(evento);
            }

            List<Evento> trilha2 = trilha1.Where(e => e.Horario == null).ToList();
            DistribuirHorarios(trilha2, true);

            Trilha2.Clear();
            trilha2 = [.. trilha2.OrderBy(x => x.Horario)];

            foreach (Evento evento in trilha2)
            {
                if (evento.Horario == null) continue;
                Trilha2.Add(evento);
            }

            List<Evento> EventosSobrando = trilha2.Where(e => e.Horario == null).ToList();
            DistribuirHorarios(EventosSobrando, false);

            PalestrasDescartadas.Clear();

            foreach (Evento evento in EventosSobrando)
            {
                if (evento.Horario == null) continue;
                PalestrasDescartadas.Add(evento);
            }

            if (EventosSobrando.Count > 0)
            {
                PopUPPalestrasNaoAgendadas(PalestrasDescartadas);
            }
        }

        private void PopUPPalestrasNaoAgendadas(List<Evento> eventosRestantes)
        {
            Form remainingEventsForm = new()
            {
                Text = "Palestras não agendadas",
                Size = new Size(400, 300),
                StartPosition = FormStartPosition.CenterParent
            };

            DataGridView dataGridViewRemaining = new()
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                DataSource = eventosRestantes
            };

            remainingEventsForm.Controls.Add(dataGridViewRemaining);
            remainingEventsForm.ShowDialog(this);
        }

        private void DistribuirHorarios(List<Evento> Trilha, bool IncluirAlmocoENetwork)
        {
            TimeOnly InicioMatutino = new(9, 0);
            TimeOnly FimMatutino = new(12, 0);
            TimeOnly InicioVespertino = new(13, 0);
            TimeOnly FimVespertino = new(17, 0);

            foreach (Evento data in Trilha)
            {

                if (InicioMatutino <= FimMatutino.AddMinutes(-data.TempoDaPalestra))
                {
                    data.Horario = InicioMatutino;
                    InicioMatutino = InicioMatutino.AddMinutes(data.TempoDaPalestra);
                    continue;
                }

                if (InicioVespertino <= FimVespertino.AddMinutes(-data.TempoDaPalestra))
                {
                    data.Horario = InicioVespertino;
                    InicioVespertino = InicioVespertino.AddMinutes(data.TempoDaPalestra);
                    continue;
                }
            }

            if (IncluirAlmocoENetwork)
            {
                Evento almoco = new()
                {
                    Horario = new TimeOnly(12, 0),
                    TempoDaPalestra = 60,
                    NomeDaPalestra = ("Almoço")
                };
                Trilha.Add(almoco);

                if (InicioVespertino < new TimeOnly(16, 0))
                {
                    InicioVespertino = new TimeOnly(16, 0);
                }

                Evento newtwork = new()
                {
                    Horario = InicioVespertino,
                    TempoDaPalestra = 60,
                    NomeDaPalestra = ("Evento De Networking")
                };
                Trilha.Add(newtwork);
            }
        }
        private void SalvarTxT(object sender, EventArgs e)
        {
            SaveFileDialog openFileDialog = new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "(*.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    using (StreamWriter writer = new(filePath))
                    {
                        writer.WriteLine("Trilha 1");
                        foreach (Evento evento in Trilha1)
                        {
                            string duracao = evento.TempoDaPalestra <= 5 ? "Relâmpago" : $"{evento.TempoDaPalestra}min";
                            writer.WriteLine($"{evento.Horario}: {evento.NomeDaPalestra} {duracao}");
                        }
                        writer.WriteLine();
                        writer.WriteLine("Trilha 2");
                        foreach (Evento evento in Trilha2)
                        {
                            string duracao = evento.TempoDaPalestra <= 5 ? "Relâmpago" : $"{evento.TempoDaPalestra}min";
                            writer.WriteLine($"{evento.Horario}: {evento.NomeDaPalestra} {duracao}");
                        }
                        writer.WriteLine();
                        writer.WriteLine("Atividades Descartadas");
                        foreach (Evento evento in PalestrasDescartadas)
                        {
                            string duracao = evento.TempoDaPalestra <= 5 ? "Relâmpago" : $"{evento.TempoDaPalestra}min";
                            writer.WriteLine($"{evento.NomeDaPalestra} {duracao}");
                        }
                    }

                    Console.WriteLine($"Lista salva em {filePath}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro salvando lista: {ex.Message}");
                }
            }
        }

        private void PalestraNome_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PalestraNome.Text))
            {
                PalestraNome.Text = "Nome da palestra";
            }
        }

        private void PalestraNome_GotFocus(object sender, EventArgs e)
        {
            if (PalestraNome.Text == "Nome da palestra")
            {
                PalestraNome.Text = "";
            }
        }

        private void PalestraTempo_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PalestraTempo.Text))
            {
                PalestraTempo.Text = "Tempo em minutos";
            }
        }

        private void PalestraTempo_GotFocus(object sender, EventArgs e)
        {
            if (PalestraTempo.Text == "Tempo em minutos")
            {
                PalestraTempo.Text = "";
            }
        }
    }
}