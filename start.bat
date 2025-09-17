@echo off
echo Checking RabbitMQ and Redis container status...

:: Kiểm tra xem Docker Desktop có đang chạy không
docker info > nul 2>&1
if errorlevel 1 (
    echo ERROR: Docker Desktop is not running. Please start Docker Desktop and try again.
    exit /b 1
)

:: --- Kiểm tra và khởi động RabbitMQ ---
echo Checking RabbitMQ...
docker ps -q -f name=rabbitmq | findstr . > nul
if errorlevel 1 (
    :: Kiểm tra xem container rabbitmq có tồn tại nhưng đã dừng không
    docker ps -aq -f name=rabbitmq | findstr . > nul
    if errorlevel 1 (
        echo No RabbitMQ container found. Starting new RabbitMQ container...
        docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 -v rabbitmq_data:/var/lib/rabbitmq rabbitmq:3-management
        if errorlevel 1 (
            echo ERROR: Failed to start new RabbitMQ container. Check Docker logs for details.
            docker logs rabbitmq
            exit /b 1
        )
    ) else (
        echo Found stopped RabbitMQ container. Starting existing RabbitMQ container...
        docker start rabbitmq
        if errorlevel 1 (
            echo ERROR: Failed to start existing RabbitMQ container. Check Docker logs for details.
            docker logs rabbitmq
            exit /b 1
        )
    )
) else (
    echo RabbitMQ container is already running.
)

:: --- Kiểm tra và khởi động Redis ---
echo Checking Redis...
docker ps -q -f name=redis | findstr . > nul
if errorlevel 1 (
    :: Kiểm tra xem container redis có tồn tại nhưng đã dừng không
    docker ps -aq -f name=redis | findstr . > nul
    if errorlevel 1 (
        echo No Redis container found. Starting new Redis container...
        docker run -d --name redis -p 6379:6379 -v redis_data:/data redis:latest
        if errorlevel 1 (
            echo ERROR: Failed to start new Redis container. Check Docker logs for details.
            docker logs redis
            exit /b 1
        )
    ) else (
        echo Found stopped Redis container. Starting existing Redis container...
        docker start redis
        if errorlevel 1 (
            echo ERROR: Failed to start existing Redis container. Check Docker logs for details.
            docker logs redis
            exit /b 1
        )
    )
) else (
    echo Redis container is already running.
)

:: Đợi vài giây để đảm bảo RabbitMQ và Redis sẵn sàng
timeout /t 5 /nobreak > nul

echo RabbitMQ is ready at localhost:5672 (AMQP) and localhost:15672 (Management UI).
echo Redis is ready at localhost:6379.
exit /b 0