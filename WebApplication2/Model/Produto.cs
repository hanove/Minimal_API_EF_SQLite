using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }

    public class ProdutoDb : DbContext
    {
        public DbSet<Produto> Produtos { get; set; } = null!;

        public ProdutoDb(DbContextOptions<ProdutoDb> options) : base(options)
        {
        }
    }
}
