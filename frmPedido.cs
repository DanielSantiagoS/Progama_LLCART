using Forms_LLCART.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Forms_LLCART.Views
{
    public partial class frmPedido : Form
    {
        private Mesa mesa;
        private Pedido pedidoAtual;

        public frmPedido(Mesa mesaSelecionada)
        {
            InitializeComponent(); // ← Este método está no Designer.cs automático
            mesa = mesaSelecionada;
            InicializarPedido();
        }

        private void InicializarPedido()
        {
            pedidoAtual = new Pedido
            {
                MesaId = mesa.Id,
                Comanda = GerarNumeroComanda(),
                GarcomResponsavel = "Usuário Atual" // Implementar autenticação depois
            };

            this.Text = $"Novo Pedido - Mesa {mesa.Numero} - Comanda: {pedidoAtual.Comanda}";
        }

        private string GerarNumeroComanda()
        {
            return DateTime.Now.ToString("ddMMyyyyHHmmss");
        }

        // ⚠️ REMOVA o método InitializeComponent() daqui!
        // Ele já está no frmPedido.Designer.cs gerado automaticamente
    }
}