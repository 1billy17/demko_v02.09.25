using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace demkobibl.Models;

public partial class DemkoBiblContext : DbContext
{
    public DemkoBiblContext()
    {
    }

    public DemkoBiblContext(DbContextOptions<DemkoBiblContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<BooksCatalog> BooksCatalogs { get; set; }

    public virtual DbSet<BooksCopy> BooksCopies { get; set; }

    public virtual DbSet<BooksGenre> BooksGenres { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientsBooksCopy> ClientsBooksCopies { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Library> Libraries { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=demko_bibl;Username=demko_bibl;Password=demko_bibl;Port=5450");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("authors_pkey");

            entity.ToTable("authors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(100)
                .HasColumnName("patronymic");
        });

        modelBuilder.Entity<BooksCatalog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("books_catalog_pkey");

            entity.ToTable("books_catalog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Pages).HasColumnName("pages");
            entity.Property(e => e.PublishYear).HasColumnName("publish_year");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.BooksCatalogs)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("books_catalog_author_id_fkey");

            entity.HasOne(d => d.Publisher).WithMany(p => p.BooksCatalogs)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("books_catalog_publisher_id_fkey");
        });

        modelBuilder.Entity<BooksCopy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("books_copies_pkey");

            entity.ToTable("books_copies");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookCatalogId).HasColumnName("book_catalog_id");
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .HasColumnName("code");
            entity.Property(e => e.DateReceipt).HasColumnName("date_receipt");
            entity.Property(e => e.LibraryId).HasColumnName("library_id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");

            entity.HasOne(d => d.BookCatalog).WithMany(p => p.BooksCopies)
                .HasForeignKey(d => d.BookCatalogId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("books_copies_book_catalog_id_fkey");

            entity.HasOne(d => d.Library).WithMany(p => p.BooksCopies)
                .HasForeignKey(d => d.LibraryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("books_copies_library_id_fkey");
        });

        modelBuilder.Entity<BooksGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("books_genres_pkey");

            entity.ToTable("books_genres");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookCatalogId).HasColumnName("book_catalog_id");
            entity.Property(e => e.GenreId).HasColumnName("genre_id");

            entity.HasOne(d => d.BookCatalog).WithMany(p => p.BooksGenres)
                .HasForeignKey(d => d.BookCatalogId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("books_genres_book_catalog_id_fkey");

            entity.HasOne(d => d.Genre).WithMany(p => p.BooksGenres)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("books_genres_genre_id_fkey");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(100)
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<ClientsBooksCopy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_books_copies_pkey");

            entity.ToTable("clients_books_copies");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookCopiesId).HasColumnName("book_copies_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.ReturnDate).HasColumnName("return_date");

            entity.HasOne(d => d.BookCopies).WithMany(p => p.ClientsBooksCopies)
                .HasForeignKey(d => d.BookCopiesId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("clients_books_copies_book_copies_id_fkey");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientsBooksCopies)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("clients_books_copies_client_id_fkey");

            entity.HasOne(d => d.Employee).WithMany(p => p.ClientsBooksCopies)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("clients_books_copies_employee_id_fkey");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.LibraryId).HasColumnName("library_id");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(100)
                .HasColumnName("patronymic");
            entity.Property(e => e.PositionId).HasColumnName("position_id");

            entity.HasOne(d => d.Library).WithMany(p => p.Employees)
                .HasForeignKey(d => d.LibraryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employees_library_id_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employees_position_id_fkey");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genres_pkey");

            entity.ToTable("genres");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("libraries_pkey");

            entity.ToTable("libraries");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.WorkSchedule)
                .HasMaxLength(255)
                .HasColumnName("work_schedule");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("positions_pkey");

            entity.ToTable("positions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("publishers_pkey");

            entity.ToTable("publishers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
