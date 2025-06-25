import { Button, Field, Input, Link, Stack } from "@chakra-ui/react";
import style from "./sign-in.module.css";

export function SignInFirst() {
  return (
    <div className={style["auth-page"]}>
      <div className={style["container"]}>
        <div className={style["container-inputs"]}>
          <Stack flex-direction="column" align="center">
            <h1 className={style["header"]}>Первый вход</h1>
            <Field.Root className={style["input"]} invalid={true} required>
              <Field.Label>
                Логин <Field.RequiredIndicator />{" "}
              </Field.Label>
              <Input placeholder="Введите ваш логин"></Input>
              <Field.ErrorText>Поле является обязательным</Field.ErrorText>
            </Field.Root>
            <Field.Root className={style["input"]} invalid={true} required>
              <Field.Label>
                Пароль <Field.RequiredIndicator />{" "}
              </Field.Label>
              <Input placeholder="Введите ваш email"></Input>
              <Field.ErrorText>Поле является обязательным</Field.ErrorText>
            </Field.Root>
            <Button colorPalette="blue" className={style["button"]}>
              Получить пароль
            </Button>
          </Stack>
        </div>
        <div className={style["container-links"]}>
          <Link variant="plain" colorPalette="blue" href="/signin">
            Вход
          </Link>
          <Link
            className={style["last-link"]}
            variant="plain"
            colorPalette="blue"
            href="/register"
          >
            Регистрация
          </Link>
        </div>
      </div>
    </div>
  );
}
