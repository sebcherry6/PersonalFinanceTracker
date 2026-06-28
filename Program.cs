using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Services;

List<Transaction> transactions = DataService.Load();

FinanceService financeService = new FinanceService(transactions);

bool running = true;

while (running)
{
    Console.WriteLine("=========================");
    Console.WriteLine("Personal Finance Tracker");
    Console.WriteLine("=========================");

    Console.WriteLine("1. Add Income");
    Console.WriteLine("2. Add Expense");
    Console.WriteLine("3. View Transactions");
    Console.WriteLine("4. View Balance");
    Console.WriteLine("5. Delete Transaction");
    Console.WriteLine("6. Exit");

    Console.Write("Choose option: ");
    string choice = Console.ReadLine();

    Console.WriteLine();

    switch (choice)
    {
        case "1":
            {
                decimal amount = InputService.ReadDecimal("Enter amount: ");
                string description = InputService.ReadString("Enter description: ");
                Category category = InputService.ReadCategory();

                var transaction = TransactionFactory.Create(
                    TransactionType.Income,
                    amount,
                    description,
                    category
                );

                financeService.AddTransaction(transaction);
                DataService.Save(financeService.GetAll());

                Console.WriteLine("Income added!");
                break;
            }

        case "2":
            {
                decimal amount = InputService.ReadDecimal("Enter amount: ");
                string description = InputService.ReadString("Enter description: ");
                Category category = InputService.ReadCategory();

                var transaction = TransactionFactory.Create(
                    TransactionType.Expense,
                    amount,
                    description,
                    category
                );

                financeService.AddTransaction(transaction);
                DataService.Save(financeService.GetAll());

                Console.WriteLine("Expense added!");
                break;
            }

        case "3":
            {
                var list = financeService.GetAll();

                if (list.Count == 0)
                {
                    Console.WriteLine("No transactions found.");
                    break;
                }

                for (int i = 0; i < list.Count; i++)
                {
                    var t = list[i];
                    Console.WriteLine($"{i + 1}) {t.Date} | {t.TransactionType} | {t.Amount} | {t.Description} | {t.Category}");
                }

                break;
            }

        case "4":
            {
                decimal balance = financeService.GetBalance();
                Console.WriteLine($"Current Balance: {balance}");
                break;
            }

        case "5":
            {
                var list = financeService.GetAll();

                if (list.Count == 0)
                {
                    Console.WriteLine("No transactions to delete.");
                    break;
                }

                for (int i = 0; i < list.Count; i++)
                {
                    var t = list[i];
                    Console.WriteLine($"{i + 1}) {t.Date} | {t.TransactionType} | {t.Amount} | {t.Description} | {t.Category}");
                }

                Console.Write("Select number to delete: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int index))
                {
                    index = index - 1;

                    if (index >= 0 && index < list.Count)
                    {
                        var transaction = list[index];
                        financeService.DeleteTransaction(transaction.Id);
                        DataService.Save(financeService.GetAll());

                        Console.WriteLine("Transaction deleted!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }

                break;
            }

        case "6":
            running = false;
            DataService.Save(financeService.GetAll());
            Console.WriteLine("Exiting...");
            break;

        default:
            Console.WriteLine("Invalid option");
            break;
    }

    Console.WriteLine();
}