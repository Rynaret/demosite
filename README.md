# demosite
Демо сайт состоит из 4 модулей. People, Poems, Reports, DemoSite.
В нём демонстрируется работа микросервисов. 

Микросервисы общаются между собой путём http запросов, а также в некоторых случаях через шину сообщений RabbitMQ. 
Получение отчетов реализовано с использованием SignalR. Пользователь отправляет запрос в DemoSite о том, что необходим отчет. DemoSite сообщает об этом. Reports получает сообщение и начинает формировать отчет. После чего при помощи SignalR приходит ответ пользователю в виде ссылки на файл.
