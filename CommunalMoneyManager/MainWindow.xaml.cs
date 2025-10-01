using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

/*
Kommuniális Pénzkezelő Alkalmazás (Osztálypénz)
*/

namespace CommunalMoneyManager
{
	public partial class MainWindow : Window
	{
		private int total = 0;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			string name = NameTextBox.Text.Trim();
			string amountText = AmountTextBox.Text.Trim();

			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(amountText))
			{
				MessageBox.Show("Kérlek, add meg a nevet és az összeget!", "Hiányzó adat", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (!int.TryParse(amountText, out int amount) || amount < 0)
			{
				MessageBox.Show("Érvényes számot adj meg az összeghez!", "Hibás adat", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			string entry = $"{name} – {amount} Ft";
			StudentListBox.Items.Add(entry);

			total += amount;
			UpdateTotalText();

			NameTextBox.Clear();
			AmountTextBox.Clear();
			NameTextBox.Focus();
		}

		private void UpdateTotalText()
		{
			TotalTextBlock.Text = $"Összesen: {total} Ft";
		}

		private void ClearFields_Click(object sender, RoutedEventArgs e)
		{
			NameTextBox.Clear();
			AmountTextBox.Clear();
			NameTextBox.Focus();
		}

		private void ClearAll_Click(object sender, RoutedEventArgs e)
		{
			StudentListBox.Items.Clear();
			total = 0;
			UpdateTotalText();
		}

		private void DeleteSelected_Click(object sender, RoutedEventArgs e)
		{
			if (StudentListBox.SelectedItem == null)
			{
				MessageBox.Show("Nincs kijelölve elem a törléshez.", "Nincs kiválasztva", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			string selectedItem = StudentListBox.SelectedItem.ToString();

			int dashIndex = selectedItem.LastIndexOf("–");
			int ftIndex = selectedItem.LastIndexOf("Ft");

			if (dashIndex != -1 && ftIndex != -1)
			{
				string amountString = selectedItem.Substring(dashIndex + 1, ftIndex - dashIndex - 1).Trim();

				if (int.TryParse(amountString, out int amount))
				{
					total -= amount;
				}
			}

			StudentListBox.Items.Remove(StudentListBox.SelectedItem);
			UpdateTotalText();
		}
	}
}
