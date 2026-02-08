# SeleniumExample

Пример реализации тестов Selenium с использованием паттерна Page Object Model (POM) и Dependency Injection.

## Описание

Это базовая обвязка для Selenium WebDriver, демонстрирующая архитектуру тестового фреймворка с чистым разделением ответственности и использованием современных подходов C#/.NET.

## Структура проекта

### Selenium.POM.Abstractions
Содержит абстракции для паттерна Page Object Model, независимые от конкретной реализации.

**Интерфейсы:**
- `IControlFactory<TWrappedItem>` - фабрика для создания контролов (элементов страницы)
  - `Create<TControl>(IWrapper<TWrappedItem> wrapper, string dataTestId)` - создание по data-test-id
  - `Create<TControl>(Func<TWrappedItem> getItem)` - создание через функцию получения элемента
  - `Create<TControl>(TWrappedItem item)` - создание напрямую из элемента

- `IPageFactory<TWrappedItem>` - фабрика для создания страниц
  - `Create<TPage>(TWrappedItem page)` - создание страницы из элемента
  - `Create<TPage>(Func<TWrappedItem> getPage)` - создание через функцию

- `IWrapper<TItem>` - обертка над элементами для абстракции от конкретной реализации

### Selenium.TestCore
Ядро тестового фреймворка с конкретной реализацией для Selenium WebDriver.

**Browser (управление браузером):**
- `IBrowserFactory` - фабрика для создания экземпляров браузера
- `IBrowserGetter` - интерфейс для получения экземпляра WebDriver
  - `GetAsync()` - асинхронное получение драйвера
- `IBrowserOptionsGetter<TOptions>` - получение опций браузера
  - `GetOptionsAsync()` - асинхронное получение конфигурации
- `DefaultBrowserProvider` - стандартная реализация провайдера браузера с поддержкой Dispose
- `DefaultChromeOptionsProvider` - стандартные опции для Chrome

**Controls (работа с элементами):**
- `IControlFactory` - расширенная фабрика контролов для Selenium
  - `Create<TControl>(IWrapper<IWebDriver> page, By by)` - создание по локатору
  - `Create<TControl>(IWebDriver page, By by)` - создание напрямую
  - `Create<TControl>(IWrapper<IWebDriver> page, string dataTestId)` - создание по data-test-id

**Dependencies (внедрение зависимостей):**
- `IDependenciesFactory` - фабрика для создания зависимостей контролов/страниц
  - `CreateDependency(Type controlType)` - создание массива зависимостей для типа
- `IDependenciesFilter` - фильтрация зависимостей
- `DependenciesFactory` - реализация с использованием IServiceProvider

**Page:**
- `ILoadable` - интерфейс для страниц с ожиданием загрузки
  - `WaitLoadAsync()` - асинхронное ожидание полной загрузки страницы

**Extensions:**
- `ServiceCollectionExtensions` - расширения для регистрации сервисов
  - `UseChrome<TFactory, TOptions>()` - регистрация Chrome браузера

## Принципы архитектуры

1. **Разделение ответственности** - абстракции отделены от реализации
2. **Dependency Injection** - все зависимости инжектируются через конструктор
3. **Page Object Model** - инкапсуляция элементов страницы в объекты
4. **Factory Pattern** - создание контролов и страниц через фабрики
5. **Async/Await** - поддержка асинхронных операций
6. **IDisposable/IAsyncDisposable** - корректное освобождение ресурсов
