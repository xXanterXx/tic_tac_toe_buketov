using System;
using System.Windows.Forms;

namespace tic_tac_toe
{
    public partial class Form4 : Form
    {
        private char currentPlayerSymbol = 'X'; // Символ текущего игрока ('X' или 'O')
        private string currentPlayerName = "Игрок 1"; // Имя текущего игрока
        private string player2Name = "Игрок 2"; // Имя второго игрока
        private Button[,] buttons; // Массив кнопок на игровом поле
        private Label resultLabel; // Метка для вывода результата игры
        private Label currentPlayerLabel; // Метка для вывода текущего игрока
        private Button restartButton; // Кнопка для начала новой игры
        private TextBox playerNameTextBox; // Текстовое поле для ввода имени первого игрока
        private TextBox player2NameTextBox; // Текстовое поле для ввода имени второго игрока


        public Form4()
        {
            InitializeComponent();
            Load += MainForm_Load; // Добавляем обработчик события Load
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            buttons = new Button[3, 3];
            resultLabel = new Label();
            currentPlayerLabel = new Label();
            restartButton = new Button();
            playerNameTextBox = new TextBox();
            player2NameTextBox = new TextBox();

            // Инициализация кнопок
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Size = new System.Drawing.Size(60, 60);
                    buttons[i, j].Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    buttons[i, j].Tag = new Tuple<int, int>(i, j);
                    buttons[i, j].Click += Cell_Click;
                    Controls.Add(buttons[i, j]);
                }
            }

            // Инициализация метки результата
            resultLabel.Size = new System.Drawing.Size(180, 30);
            resultLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            resultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Controls.Add(resultLabel);

            // Инициализация метки текущего игрока
            currentPlayerLabel.Size = new System.Drawing.Size(180, 30);
            currentPlayerLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            currentPlayerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Controls.Add(currentPlayerLabel);

            // Инициализация текстового поля для ввода имени первого игрока
            playerNameTextBox.Size = new System.Drawing.Size(180, 30);
            playerNameTextBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            playerNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            playerNameTextBox.Text = "Игрок 1"; // Имя по умолчанию
            playerNameTextBox.TextChanged += PlayerNameTextBox_TextChanged;
            Controls.Add(playerNameTextBox);

            // Инициализация текстового поля для ввода имени второго игрока
            player2NameTextBox.Size = new System.Drawing.Size(180, 30);
            player2NameTextBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            player2NameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            player2NameTextBox.Text = "Игрок 2"; // Имя по умолчанию
            player2NameTextBox.TextChanged += Player2NameTextBox_TextChanged;
            Controls.Add(player2NameTextBox);

            // Инициализация кнопки "Начать заново"
            restartButton.Size = new System.Drawing.Size(100, 30);
            restartButton.Text = "Начать заново";
            restartButton.Click += RestartButton_Click;
            Controls.Add(restartButton);

            // Вызов метода для центрирования элементов при инициализации
            CenterElements();
        }

        private void CenterElements()
        {
            if (buttons != null)
            {
                int formCenterX = Width / 2;
                int formCenterY = Height / 2;

                // Центрирование кнопок на игровом поле
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        buttons[i, j].Left = formCenterX - buttons[i, j].Width / 2 + (j - 1) * buttons[i, j].Width;
                        buttons[i, j].Top = formCenterY - buttons[i, j].Height / 2 + (i - 1) * buttons[i, j].Height;
                    }
                }

                // Центрирование метки результата
                resultLabel.Left = formCenterX - resultLabel.Width / 2;
                resultLabel.Top = formCenterY - resultLabel.Height / 2 - 180;

                // Центрирование метки текущего игрока
                currentPlayerLabel.Left = formCenterX - currentPlayerLabel.Width / 2;
                currentPlayerLabel.Top = formCenterY - currentPlayerLabel.Height / 2 - 150;

                // Центрирование текстового поля для ввода имени первого игрока
                playerNameTextBox.Left = formCenterX - playerNameTextBox.Width / 2 - 100;
                playerNameTextBox.Top = formCenterY - playerNameTextBox.Height / 2 + 190;

                // Центрирование текстового поля для ввода имени второго игрока
                player2NameTextBox.Left = formCenterX - player2NameTextBox.Width / 2 + 100;
                player2NameTextBox.Top = formCenterY - player2NameTextBox.Height / 2 + 190;

                // Центрирование кнопки "Начать заново"
                restartButton.Left = formCenterX - restartButton.Width / 2;
                restartButton.Top = formCenterY - restartButton.Height / 2 + 150;
            }
        }

        private void PlayerNameTextBox_TextChanged(object sender, EventArgs e)
        {
            // Обновление имени текущего игрока при изменении текста в текстовом поле
            currentPlayerName = playerNameTextBox.Text;
            currentPlayerLabel.Text = $"Ход игрока: {currentPlayerName}";
        }

        private void Player2NameTextBox_TextChanged(object sender, EventArgs e)
        {
            // Обновление имени второго игрока при изменении текста в текстовом поле
            player2Name = player2NameTextBox.Text;
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            Button clickedCell = (Button)sender;

            // Проверка, не занята ли ячейка
            if (clickedCell.Text == "")
            {
                // Установка символа в ячейку
                clickedCell.Text = currentPlayerSymbol.ToString();

                // Проверка условий победы
                if (CheckWin())
                {
                    resultLabel.Text = $"Победил игрок {currentPlayerName}!";
                    DisableCells();
                }
                else if (CheckDraw())
                {
                    resultLabel.Text = "Ничья!";
                }
                else
                {
                    // Смена текущего игрока
                    currentPlayerSymbol = (currentPlayerSymbol == 'X') ? 'O' : 'X';
                    currentPlayerLabel.Text = $"Ход игрока: {(currentPlayerSymbol == 'X' ? currentPlayerName : player2Name)}";
                }
            }
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            // Очистка ячеек
            foreach (Button button in buttons)
            {
                button.Text = "";
            }

            // Сброс результатов и текущего игрока
            currentPlayerSymbol = 'X';
            currentPlayerName = playerNameTextBox.Text;
            currentPlayerLabel.Text = $"Ход игрока: {currentPlayerName}";

            // Обновление имени второго игрока
            player2Name = player2NameTextBox.Text;

            // Обнуление метки результата
            resultLabel.Text = "";

            EnableCells();
        }

        private bool CheckWin()
        {
            // Проверка на победу по горизонтали
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Text != "" &&
                    buttons[i, 0].Text == buttons[i, 1].Text &&
                    buttons[i, 1].Text == buttons[i, 2].Text)
                {
                    return true;
                }
            }

            // Проверка на победу по вертикали
            for (int j = 0; j < 3; j++)
            {
                if (buttons[0, j].Text != "" &&
                    buttons[0, j].Text == buttons[1, j].Text &&
                    buttons[1, j].Text == buttons[2, j].Text)
                {
                    return true;
                }
            }

            // Проверка на победу по диагонали
            if (buttons[0, 0].Text != "" &&
                buttons[0, 0].Text == buttons[1, 1].Text &&
                buttons[1, 1].Text == buttons[2, 2].Text)
            {
                return true; // Победа в диагонали (слева направо)
            }

            if (buttons[0, 2].Text != "" &&
                buttons[0, 2].Text == buttons[1, 1].Text &&
                buttons[1, 1].Text == buttons[2, 0].Text)
            {
                return true; // Победа в диагонали (справа налево)
            }

            return false;
        }

        private bool CheckDraw()
        {
            // Проверка на ничью (все ячейки заняты)
            foreach (Button button in buttons)
            {
                if (button.Text == "")
                {
                    return false; // Есть свободная ячейка
                }
            }

            return true; // Все ячейки заняты, но нет победителя
        }

        private void DisableCells()
        {
            // Отключение всех ячеек после завершения игры
            foreach (Button button in buttons)
            {
                button.Enabled = false;
            }
        }

        private void EnableCells()
        {
            // Включение всех ячеек перед началом новой игры
            foreach (Button button in buttons)
            {
                button.Enabled = true;
            }
        }


    }
}
