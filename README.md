# Telegram Bot (Pet Project)

## Описание
Это pet-проект — Telegram-бот, разработанный на **C#** с использованием **.NET 8.0**. Бот предназначен для обработки сообщений пользователей, сохранения данных в **MySQL** и кэширования в **Redis**. Проект демонстрирует базовые навыки работы с микросервисной архитектурой, контейнеризацией (**Docker**), логированием (**Serilog**) и юнит-тестированием.

**Основные функции:**
- Регистрация пользователей и сохранение сообщений в базе данных.
- Логирование действий в файл и отправка логов в Telegram.
- Кэширование последних сообщений в Redis.
- Обработка команд (`/start`, `/testError`) и callback-запросов.

Проект создан как часть портфолио Junior-разработчика.

## Технологии
- **Язык**: C# (.NET 8.0)
- **Библиотеки**:
  - `Telegram.Bot` — для работы с Telegram API.
  - `EntityFrameworkCore` и `Pomelo.EntityFrameworkCore.MySql` — для работы с MySQL.
  - `StackExchange.Redis` — для кэширования в Redis.
  - `Serilog` — для логирования.
  - `Polly` — для обработки ошибок с повторными попытками.
- **База данных**: MySQL 8.0
- **Кэш**: Redis 7.4.2
- **Контейнеризация**: Docker, Docker Compose
- **Тестирование**: xUnit с in-memory базой данных

## Структура проекта
- **Config/**: Файл `appsettings.json` с конфигурацией (токены, строки подключения).
- **DB/**:
  - **Data/**: `AppDbContext` для настройки EF Core.
  - Модели: `User` и `Message` с полями для таблиц.
- **Migration/**: Автосгенерированные миграции для БД.
- **Services/**:
  - `BotService.cs`: Фоновая служба для запуска бота.
  - `DbService.cs`: Логика работы с базой данных.
  - `MessageSender.cs`: Отправка логов в Telegram.
  - `RedisService.cs`: Работа с кэшем Redis.
  - `UpdateHandler.cs`: Обработка обновлений от Telegram.
  - `IMessageSender.cs`: Интерфейс для отправки сообщений.
- **mysql-init/**: `init.sql` для инициализации БД и пользователя.
- **Logs/**: Папка для хранения логов.
- **Program.cs**: Точка входа, настройка DI и хостинга.
- **Tests/**: Юнит-тесты для `DbService`.
- **.env**: Скрытые переменные окружения (не включены в репозиторий).
- **docker-compose.yml**: Конфигурация Docker-сервисов.
- **Dockerfile**: Инструкции для сборки контейнера.

## Требования
- .NET 8.0 SDK
- Docker и Docker Compose
- MySQL 8.0 (или через Docker)
- Redis 7.4.2 (или через Docker)

## Установка и запуск
1. **Клонируйте репозиторий:**
   ```bash
   git clone <repository-url>
   cd <repository-folder>
   ```
Настройте переменные окружения: Создайте файл .env в корне проекта и добавьте следующие переменные:
text

Collapse

Wrap

Copy
TELEGRAM_BOT_TOKEN=<ваш_токен_бота>
TELEGRAM_LOG_CHAT_ID=<ID_чата_для_логов>
MYSQL_CONNECTION_STRING=Server=db;Database=telegembot;User=botuser;Password=<ваш_пароль>;
REDIS_CONNECTION_STRING=redis:6379
MYSQL_ROOT_PASSWORD=<пароль_root>
MYSQL_BOTUSER_PASSWORD=<пароль_botuser>
Запустите проект с помощью Docker Compose:
bash

Collapse

Wrap

Copy
docker-compose up --build
Это соберет и запустит контейнеры для бота, MySQL и Redis. База данных будет автоматически инициализирована через init.sql.
Логирование: Логи сохраняются в Logs/log.txt и отправляются в указанный Telegram-чат.
Остановка:
bash

Collapse

Wrap

Copy
docker-compose down
Тестирование
Юнит-тесты находятся в отдельном проекте DbServiceTests. Для запуска:

bash

Collapse

Wrap

Copy
cd Tests
dotnet test
Тесты проверяют сохранение сообщений и добавление пользователей в БД с использованием in-memory базы данных.

Использование
Отправьте /start для приветственного сообщения.
Отправьте любое сообщение — оно будет сохранено в Redis и MySQL.
Нажмите кнопку в ответном сообщении для теста callback-запроса.
Используйте /testError для проверки обработки ошибок.
Примечания
Проект использует Polly для повторных попыток при сбоях БД или Redis.
Логирование настроено через Serilog с выводом в консоль и файл.
Миграции применяются автоматически при старте приложения.
Автор
<Ваше имя> — Junior C# разработчик.
