using ReceitaMx.Models;

namespace ReceitaMx.ViewModels;

public class HomeViewModel
{
    public List<Receita> receitas { get; set; }
    public List<Categoria> categorias { get; set; }
}