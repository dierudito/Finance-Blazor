﻿using System.ComponentModel.DataAnnotations;

namespace FinaFlow.Core.Requests.Categories;

public class CreateCategoryRequest : Request
{
    [Required(ErrorMessage = "Título inválido")]
    [MaxLength(80, ErrorMessage = "O título deve conter até 80 caracteres")]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
