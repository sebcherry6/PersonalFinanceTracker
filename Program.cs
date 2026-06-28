using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Services;

List<Transaction> transactions = new();

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
    Console.WriteLine("5. Exit");

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

                Console.WriteLine("Expense added!");
                break;
            }

        case "3":
            {
                foreach (var t in financeService.GetAll())
                {
                    Console.WriteLine($"{t.Date} | {t.TransactionType} | {t.Amount} | {t.Description} | {t.Category}");
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
            running = false;
            Console.WriteLine("Exiting...");
            break;

        default:
            Console.WriteLine("Invalid option");
            break;
    }

    Console.WriteLine();
}