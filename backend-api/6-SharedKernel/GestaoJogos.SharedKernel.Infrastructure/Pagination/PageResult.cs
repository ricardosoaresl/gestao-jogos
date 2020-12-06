using System;
using System.Collections.Generic;

namespace GestaoJogos.SharedKernel.Infrastructure.Pagination
{
    public class PagedResult<TEntity> where TEntity : class
    {
        /// <summary>
        /// Itens retornados da consulta
        /// </summary>
        public IEnumerable<TEntity> Items { get; set; }

        /// <summary>
        /// Numero da pagina atual
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Quantidade de itens em cada pagina
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Numero total de itens
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Numero total de paginas
        /// </summary>
        /// 
        public int TotalPages()
        {
            return (int)(Math.Ceiling((Decimal)TotalItems / (Decimal)PageSize));
        }

        /// <summary>
        /// Informa a página anterior
        /// </summary>
        /// <returns>true/false</returns>
        public int Prev()
        {
            return Page - 1;
        }

        /// <summary>
        /// Informa a próxima página
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            if (Page < TotalPages())
                return Page + 1;
            else
                return 0;
        }

    }
}
