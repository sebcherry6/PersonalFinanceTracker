using System;
using System.Collections.Generic;
using System.Text;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Services;


    public class FinanceService
    {
        private readonly List<Transaction> _transactions;

        public FinanceService(List<Transaction> transactions)
        {
            _transactions = transactions;
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public List<Transaction> GetAll()
        {
            return _transactions;
        }

        public decimal GetBalance()
        {
            decimal income = _transactions
                .Where(t => t.TransactionType == TransactionType.Income)
                .Sum(t => t.Amount);

            decimal expense = _transactions
                .Where(t => t.TransactionType == TransactionType.Expense)
                .Sum(t => t.Amount);

            return income - expense;
        }
        public void DeleteTransaction(Guid id)
        {
            var transaction = _transactions.FirstOrDefault(t => t.Id == id);

            if (transaction != null)
            {
                _transactions.Remove(transaction);
            }
        }
        public bool EditTransaction(
        Guid id,
        decimal amount,
        string description,
        Category category)
        {
            Transaction? transaction = _transactions.FirstOrDefault(t => t.Id == id);

            if (transaction == null)
                return false;

            transaction.Amount = amount;
            transaction.Description = description;
            transaction.Category = category;

            return true;
        }
        public List<Transaction> GetByCategory(Category category)
        {
            return _transactions
                .Where(t => t.Category == category)
                .ToList();
        }

        public List<Transaction> GetByType(TransactionType type)
        {
            return _transactions
                .Where(t => t.TransactionType == type)
                .ToList();
        }
        public decimal GetTotalIncome()
        {
            return _transactions
                .Where(t => t.TransactionType == TransactionType.Income)
                .Sum(t => t.Amount);
        }

        public decimal GetTotalExpense()
        {
            return _transactions
                .Where(t => t.TransactionType == TransactionType.Expense)
                .Sum(t => t.Amount);
        }
}
