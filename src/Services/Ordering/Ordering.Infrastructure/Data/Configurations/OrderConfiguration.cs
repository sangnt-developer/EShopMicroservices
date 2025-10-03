﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(p => p.CustomerId)
            .IsRequired();

        builder.HasMany(p => p.OrderItems)
            .WithOne()
            .HasForeignKey(p => p.OrderId);

        builder.ComplexProperty(p => p.OrderName, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.ComplexProperty(p => p.ShippingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.LastName)
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(100);

            addressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

            addressBuilder.Property(a => a.Country)
                .HasMaxLength(50);

            addressBuilder.Property(a => a.State)
                .HasMaxLength(50);

            addressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.ComplexProperty(p => p.BillingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.LastName)
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(100);

            addressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

            addressBuilder.Property(a => a.Country)
                .HasMaxLength(50);

            addressBuilder.Property(a => a.State)
                .HasMaxLength(50);

            addressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.Payment, paymentBuilder =>
        {
            paymentBuilder.Property(p => p.CardName)
                .HasMaxLength(50);

            paymentBuilder.Property(p => p.CardNumber)
                .HasMaxLength(24)
                .IsRequired();

            paymentBuilder.Property(p => p.Expiration)
                .HasMaxLength(10);

            paymentBuilder.Property(p => p.CVV)
                .HasMaxLength(3);

            paymentBuilder.Property(p => p.PaymentMethod);
        });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                s => s.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.Property(o => o.TotalPrice);
    }
}
