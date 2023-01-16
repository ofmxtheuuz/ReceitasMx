using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReceitaMx.Models;

namespace ReceitaMx.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    // Tabela receitas
    public DbSet<Receita> Receitas { get; set; }
    // Tabela categorias
    public DbSet<Categoria> Categorias { get; set; }
}
