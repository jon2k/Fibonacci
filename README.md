Fibonacci

1. Для работы приложения необходимо запустить RabbitMq. Удобно делать в docker. Для этого необходимо выполнить следующие команды:
- docker run -d --hostname my-rabbit --name some-rabbit -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password rabbitmq:3-management
- docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management

2. Запустить приложения FibonacciFirst и FibonacciSecond.

3. Метод Post приложения FibonacciFirst получает заданное количество вычислений и начинает вычислять.

4. Метод Get приложения FibonacciFirst получает желаемое количество вычислений и возвращает вычисленную последовательность чисел Фибоначи при условии, что они уже вычислены.

P/s: DockerCompose пока не работает.