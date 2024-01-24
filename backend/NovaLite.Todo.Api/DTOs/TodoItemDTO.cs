﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using NovaLite.Todo.Api.Model;

namespace NovaLite.Todo.Api.DTOs
{
    public class TodoItemDTO
    { 
        public required string Content { get; set; }
        public Guid TodoListId { get; set; }
        public Status Status { get; set; }
    }
}
