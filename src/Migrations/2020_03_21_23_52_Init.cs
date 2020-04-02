using SimpleMigrations;

namespace Ascalon.ClientService.Migrations.Migrations
{
    [Migration(2020_03_21_23_52, "init db")]
    public class _2020_03_21_23_52_Init : Migration
    {
        protected override void Up()
        {
            Execute(@"
            CREATE TABLE roles (
                id                          serial              primary key,
                name                        varchar(50)         NOT NULL
            );
            COMMENT ON TABLE roles IS 'Список ролей для клиентов';
            COMMENT ON COLUMN roles.name IS 'Наименование роли';            

            CREATE TABLE users (
                id                          serial              primary key,
                login                       varchar(50)         NOT NULL,
                password                    varchar(50)         NOT NULL,
                role_id                     int                 NOT NULL,
                full_name                   varchar(150)        NOT NULL,
                dumper_id                   int,
                CONSTRAINT  users_role_id_fk FOREIGN KEY (role_id) REFERENCES roles(id)
            );
            COMMENT ON TABLE users IS 'Список пользователей с их данными';
            COMMENT ON COLUMN users.login IS 'Идентификатор для авторизации пользователя';
            COMMENT ON COLUMN users.password IS 'Пароль идентификатора пользователя';
            COMMENT ON COLUMN users.role_id IS 'Роль пользователя в системе';
            COMMENT ON COLUMN users.full_name IS 'ФИО пользователя';
            
            CREATE TABLE tasks (
                id                          serial              primary key,
                driver_id                   int                 NOT NULL,
                description                 varchar(100)        NOT NULL,
                start_longitude             real                NOT NULL,
                start_latitude              real                NOT NULL,
                end_longitude               real                NOT NULL,
                end_latitude                real                NOT NULL,
                status                      smallint            NOT NULL,
                entity                      varchar(100)        NOT NULL,
                created_at                  timestamp           NOT NULL,
                CONSTRAINT tasks_driver_id_fk FOREIGN KEY (driver_id) REFERENCES users(id)
            );

            COMMENT ON TABLE tasks IS 'Список задач';
            COMMENT ON COLUMN tasks.driver_id IS 'Идентификатор водителя';
            COMMENT ON COLUMN tasks.description IS 'Описание задания';
            COMMENT ON COLUMN tasks.start_longitude IS 'Долгота начала задания';
            COMMENT ON COLUMN tasks.start_latitude IS 'Широта начала задания';
            COMMENT ON COLUMN tasks.end_longitude IS 'Долгота конца задания';
            COMMENT ON COLUMN tasks.end_latitude IS 'Широта конца задания';
            COMMENT ON COLUMN tasks.status IS 'Статус задания';
            COMMENT ON COLUMN tasks.entity IS 'Основное задание';
            COMMENT ON COLUMN tasks.created_at IS 'Дата и время создания задания';
            
               
            INSERT INTO roles (name) VALUES ('Logist'),('Driver');
            INSERT INTO users (login, password, role_id, full_name) VALUES ('a.mirko@dostaevsky.ru', '123456', 1, 'Мирко А.А.');
            INSERT INTO users (login, password, role_id, full_name) VALUES ('test@gmail.com', '654321', 2, 'DriverTest');
            ");
        }

        protected override void Down()
        {
            Execute(@"
                DROP TABLE roles;

                DROP TABLE users;

                DROP TABLE tasks;
                ");
        }
    }
}
