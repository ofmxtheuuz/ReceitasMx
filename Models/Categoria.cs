using System.ComponentModel.DataAnnotations;

namespace ReceitaMx.Models;

public class Categoria
{
    [Display(Name = "Categoria")]
    public int CategoriaId { get; set; }
    [Display(Name = "Titulo da categoria")]
    public string Title { get; set; }
    [Display(Name = "Descrição da categoria")]
    public string Description { get; set; }
}