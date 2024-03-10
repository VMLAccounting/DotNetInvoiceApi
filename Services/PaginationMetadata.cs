namespace CityInfo.API.Services
{
    /// <summary>
    /// Represents the metadata for pagination.
    /// </summary>
    public class PaginationMetadata
    {
        /// <summary>
        /// Gets or sets the total item count.
        /// </summary>
        public int TotalItemCount { get; set; }

        /// <summary>
        /// Gets or sets the total page count.
        /// </summary>
        public int TotalPageCount { get; set; }
        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginationMetadata"/> class.
        /// </summary>
        /// <param name="totalItemCount">The total item count.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="currentPage">The current page number.</param>
        public PaginationMetadata(int totalItemCount, int pageSize, int currentPage)
        {
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        }
    }
}
