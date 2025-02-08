﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace news_agregator.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Model.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("ArticleId")
                        .HasColumnType("int")
                        .HasColumnName("articleId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("createdAt");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("text");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Model.NewsArticle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("author");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("content");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("publishedAt");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("url");

                    b.Property<string>("UrlToImage")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("UrlToImage");

                    b.HasKey("Id");

                    b.ToTable("NewsArticles");
                });

            modelBuilder.Entity("Model.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Settings")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("setting");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("type");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Model.SavedArticle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SavedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("savedAt");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("SavedArticle");
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<bool>("Administrator")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("administrator");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("createdAt");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("hashPassword")
                        .HasAnnotation("Relational:JsonPropertyName", "password");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Model.Comment", b =>
                {
                    b.HasOne("Model.NewsArticle", "NewsArticle")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NewsArticle");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.NewsArticle", b =>
                {
                    b.OwnsOne("Source", "Source", b1 =>
                        {
                            b1.Property<int>("NewsArticleId")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("NewsArticleId");

                            b1.ToTable("NewsArticles");

                            b1.WithOwner()
                                .HasForeignKey("NewsArticleId");
                        });

                    b.Navigation("Source")
                        .IsRequired();
                });

            modelBuilder.Entity("Model.Notification", b =>
                {
                    b.HasOne("Model.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.SavedArticle", b =>
                {
                    b.HasOne("Model.NewsArticle", "NewsArticle")
                        .WithMany("SavedArticles")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.User", "User")
                        .WithMany("SavedArticles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NewsArticle");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.NewsArticle", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("SavedArticles");
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Notifications");

                    b.Navigation("SavedArticles");
                });
#pragma warning restore 612, 618
        }
    }
}
