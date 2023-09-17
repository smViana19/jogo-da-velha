using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jogo_da_velha_com_matriz
{
	public partial class JogoDaVelha : Form
	{
		//declaração das variaveis globais
		int PontosX = 0, PontosO = 0, Empates = 0, rodadas = 0;

		bool VezDeJogar = true, jogo_final = false;    //vez que o jogador X ou O jogara 

		string[,] texto = new string[3,3];

		public JogoDaVelha()
		{
			//Criação da matriz vazia para utilizar os botões
			InitializeComponent();
			for (int linha = 0; linha < 3; linha++)
			{
				for (int coluna = 0; coluna < 3; coluna++)
				{
					texto[linha, coluna] = "";
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			
			Button btn = (Button)sender; // Botao clicavel para qualquer botao apartir deste objeto
			int buttonIndex = btn.TabIndex;
			int linha = buttonIndex / 3;
			int coluna = buttonIndex % 3;

			if (btn.Text == "" && jogo_final == false)      //If para nao ficar substituindo a vez de jogar 
			{
				//estrura Operador Ternário para resumir o codigo do IF ELSE
				string jogadorAtual = VezDeJogar ? "X" : "O";
				btn.Text = jogadorAtual;
				texto[linha, coluna] = btn.Text ;
				rodadas++;
				VezDeJogar = !VezDeJogar;
				lblVezDoJogador.Text = "Vez do Jogador: " + (VezDeJogar ? "X" : "O");
				Checagem(jogadorAtual);
			}
		}
		void Checagem(string jogadorAtual)
		{
			// Verificar vitória por linhas, colunas e diagonais
			if (VitoriaPorLinha(jogadorAtual) || VitoriaPorColuna(jogadorAtual) || VitoriaPorDiagonalPrincipal(jogadorAtual) || VitoriaPorDiagonalSecundaria(jogadorAtual))
				{
					MessageBox.Show("Jogador " + jogadorAtual + " ganhou!");
					AtualizarPontuacao(jogadorAtual);
					jogo_final = true;
				}
			else if (VerificarEmpate())
			{
				MessageBox.Show("Deu Véia!");
				AtualizarEmpates();
				jogo_final = true;
			}
		}

		bool VitoriaPorLinha(string jogador)
		{
			for (int linha = 0; linha < texto.GetLength(0); linha++)
			{
				bool linhaCompleta = true;
				for (int coluna = 0; coluna < texto.GetLength(1); coluna++)
				{
					if (texto[linha, coluna] != jogador)
					{
						linhaCompleta = false;
						break;
					}
				}
				if (linhaCompleta)
				{
					return true;
				}
			}
			return false;
		}

		bool VitoriaPorColuna(string jogador)
		{
			for (int coluna = 0; coluna < texto.GetLength(1); coluna++)
			{
				bool colunaCompleta = true;
				for (int linha = 0; linha < texto.GetLength(0); linha++)
				{
					if (texto[linha, coluna] != jogador)
					{
						colunaCompleta = false;
						break;
					}
				}
				if (colunaCompleta)
				{
					return true;
				}
			}
			return false;
		}

		bool VitoriaPorDiagonalPrincipal(string jogador)
		{
			for (int i = 0; i < texto.GetLength(0); i++)
			{
				if (texto[i, i] != jogador)
				{
					return false;
				}
			}

			return true;
		}

		bool VitoriaPorDiagonalSecundaria(string jogador)
		{
			for (int i = 0; i < texto.GetLength(0); i++)
			{
				if (texto[i, texto.GetLength(1) - 1 - i] != jogador)
				{
					return false;
				}
			}

			return true;
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			  

			// Redefinir a pontuação dos empates para zero
			Empates = 0;
			EmpatePontos.Text = "0";
			PontosO = 0;
			Opontos.Text = "0";
			PontosX = 0;
			Xpontos.Text = "0";

		}

		private void JogoDaVelha_Load(object sender, EventArgs e)
		{
			lblVezDoJogador.Text = "Vez do Jogador: X";
		}

		bool VerificarEmpate()
		{
			for (int linha = 0; linha < texto.GetLength(0); linha++)
			{
				for (int coluna = 0; coluna < texto.GetLength(1); coluna++)
				{
					if (texto[linha, coluna] == "")
					{
						return false; // Ainda há células vazias, o jogo não está em empate
					}
				}
			}

			return true; // Todas as células estão preenchidas, o jogo está em empate
		}

		private void button10_Click(object sender, EventArgs e)
		{
			foreach (Control control in panel1.Controls)
			{
				if (control is Button btn)
				{
					btn.Text = "";
				}
			}
			lblVezDoJogador.Text = "Vez do Jogador: X";

			// Redefinir variáveis de controle
			jogo_final = false;
			VezDeJogar = true;
			rodadas = 0;

			// Redefinir a matriz de texto para o estado inicial (vazia)
			for (int linha = 0; linha < 3; linha++)
			{
				for (int coluna = 0; coluna < 3; coluna++)
				{
					texto[linha, coluna] = "";
				}
			}

			// Redefinir as pontuações para zero
			
		}
		void AtualizarEmpates()
		{
			Empates++;
			EmpatePontos.Text =  Convert.ToString(Empates);
		}

		void AtualizarPontuacao(string jogador)
		{
			if (jogador == "X")
			{
				PontosX++;
				Xpontos.Text = Convert.ToString(PontosX); 

			}
			else if (jogador == "O")
			{
				PontosO++;
				Opontos.Text = Convert.ToString(PontosO);
			}
			
		}

	}
}

