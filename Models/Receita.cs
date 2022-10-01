using System.ComponentModel.DataAnnotations;

namespace ReceitaMx.Models;

public class Receita
{
    [Display(Name = "Id da receita")]
    public int ReceitaId { get; set; }
    [Display(Name = "Titulo da receita")]
    public string Title { get; set; }
    [Display(Name = "Descrição da receita")]
    public string Description { get; set; }
    [Display(Name = "Passo a passo")]
    public string Guide { get; set; }
    public Categoria Categoria { get; set; }
    [Display(Name = "Categoria da receita")]
    public int CategoriaId { get; set; }
    [Display(Name = "Data de envio")]
    public DateTime SendDate { get; set; }
}