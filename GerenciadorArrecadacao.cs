using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToPark
{
    class GerenciadorArrecadacao
    { 
        public float ValorHora { get; set; } // Para configurar o valor da hora dinamicamente
        private float _arrecadado; // para armazenar o total arrecadado

        public float Arrecadado
        {
            get => _arrecadado; //simplesmente retorna o valor da variavel "_arrecadado"
            set => _arrecadado += value; // soma o valor atual da varial _arrecadado com o valor recebido e salva este valor da variavel
        }

        public float CalcularEstadiaCliete(DateTime entrada)
        {
            var permanencia = DateTime.Now - entrada; // calcula o tempo de permanencia desde a entrada ate o presente momento

            if (permanencia.Hours <= 0)//se a estadia foi menor que 1hr, simplesmente retorna o valor de 1hr
                return ValorHora;
            else // senao, calcular o respectivo valor de acordo com o tempo da estadia
                return ValorHora * permanencia.Hours;
        }

    }
}
