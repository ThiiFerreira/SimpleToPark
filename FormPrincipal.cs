using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace SimpleToPark
{
    public partial class FormPrincipal : Form
    {

        private DataTable bancoDeDados;
        private GerenciadorArrecadacao gerenciador;

        public FormPrincipal()
        {
            InitializeComponent();

            bancoDeDados = new DataTable("Estacionamento");
            bancoDeDados.Columns.Add("Placa", typeof(string));
            bancoDeDados.Columns.Add("Entrada", typeof(string));

            dataGridViewCarrosEstacionados.DataSource = bancoDeDados;

            gerenciador = new GerenciadorArrecadacao
            {
                ValorHora = 10,
                Arrecadado = 0
            };

            labelValorHora.Text = $"Valor da hora: R${gerenciador.ValorHora.ToString("0.00")}";
             

        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {
            var placa = Interaction.InputBox("Digite a placa do veículo:", "Entrada de veículo");
            if (!string.IsNullOrEmpty(placa))
            {
                bancoDeDados.Rows.Add(new string[] { placa, DateTime.Now.ToString()});
                dataGridViewCarrosEstacionados.Rows[dataGridViewCarrosEstacionados.Rows.Count - 1].MinimumHeight = 30;
            }
        }

        private void buttonConfigurar_Click(object sender, EventArgs e)
        {
            var valorDaHora = Interaction.InputBox("Digite o valor da hora:", "Valor da hora");
            if (!string.IsNullOrEmpty(valorDaHora))
            {
                gerenciador.ValorHora = float.Parse(valorDaHora);
                labelValorHora.Text = $"Valor da hora: R${gerenciador.ValorHora.ToString("0.00")}";

            }
        }

        private void dataGridViewCarrosEstacionados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           if(e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //recupera a hora de entrada e a placa do veiculo
                var entrada = DateTime.Parse(bancoDeDados.Rows[e.RowIndex].ItemArray[1].ToString());
                var placa = bancoDeDados.Rows[e.RowIndex].ItemArray[0].ToString();

                var arrecadado = gerenciador.CalcularEstadiaCliete(entrada); //calcular o valor que o cliente deverar pagar

                //exibi mensagem
                if(MessageBox
                    .Show(this,$"Deseja encerrar o Ticket de {placa}? Valor: R${arrecadado.ToString("0.00")}", "Encerrar Ticket", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    //remove do banco de dados
                    bancoDeDados.Rows.RemoveAt(e.RowIndex);
                    //arrecada o valor
                    gerenciador.Arrecadado = arrecadado;
                    //atualizar o valor na tela
                    labelValorArrecadado.Text = $"Valor Arrecadado: R${gerenciador.Arrecadado.ToString("0.00")}";


                }
            }
        }
    }
}
