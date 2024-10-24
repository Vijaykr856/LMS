namespace LMS.Models
{
    
        public class Book
        {
            public int BookId { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Genre { get; set; }
            public int TotalCopies { get; set; }
            public int AvailableCopies { get; set; }
        }

        public class IssuedBook
        {
            public int IssuedBookId { get; set; }
            public int BookId { get; set; }
            public string IssuedTo { get; set; }
            public DateTime IssueDate { get; set; }
            public DateTime? ReturnDate { get; set; }
            public Book Book { get; set; }
        }

    }

