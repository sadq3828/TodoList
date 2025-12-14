using System;
using System.ComponentModel.DataAnnotations;
public class Todo
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    public string? Description { get; set; }

    public bool IsDone { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}