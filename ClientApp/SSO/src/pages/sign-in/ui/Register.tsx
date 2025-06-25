import { Button, Field, Input, Link, Stack } from "@chakra-ui/react";
import style from "./sign-in.module.css";
import { PasswordInput } from "../../../components/ui/password-input";

export function Register() {
  return (
    <div className={style["auth-page"]}>
      <div className={style["container"]}>
        <div className={style["container-inputs"]}>
          <Stack flex-direction="column" align="center">
            <h1 className={style["header"]}>Регистрация</h1>
            <Field.Root className={style["input"]} invalid={false} required>
              <Field.Label>
                Имя <Field.RequiredIndicator />{" "}
              </Field.Label>
              <Input placeholder="Введите ваше имя"></Input>
              <Field.ErrorText>Поле является обязательным</Field.ErrorText>
            </Field.Root>
            <Field.Root className={style["input"]} invalid={false} required>
              <Field.Label>
                Фамилия <Field.RequiredIndicator />{" "}
              </Field.Label>
              <Input placeholder="Введите вашу фамилию"></Input>
              <Field.ErrorText>Поле является обязательным</Field.ErrorText>
            </Field.Root>
            <Field.Root className={style["input"]} invalid={false} required>
              <Field.Label>Отчество</Field.Label>
              <Input placeholder="Введите ваше отчество"></Input>
              <Field.ErrorText>Поле является обязательным</Field.ErrorText>
            </Field.Root>
            <Field.Root className={style["input"]} invalid={false} required>
              <Field.Label>
                Логин <Field.RequiredIndicator />{" "}
              </Field.Label>
              <Input placeholder="Введите ваш логин"></Input>
              <Field.ErrorText>Поле является обязательным</Field.ErrorText>
            </Field.Root>
            <Field.Root className={style["input"]} invalid={false} required>
              <Field.Label>
                Email <Field.RequiredIndicator />{" "}
              </Field.Label>
              <Input placeholder="Введите ваш email"></Input>
              <Field.ErrorText>Поле является обязательным</Field.ErrorText>
            </Field.Root>
            <Field.Root className={style["input"]} invalid={false} required>
              <Field.Label>
                Пароль <Field.RequiredIndicator />{" "}
              </Field.Label>
              <PasswordInput placeholder="Введите ваш пароль"></PasswordInput>
              <Field.ErrorText>Поле является обязательным</Field.ErrorText>
            </Field.Root>
            <Button colorPalette="blue" className={style["button"]}>
              Зарегистрироваться
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
            href="/signinfirst"
          >
            Первый вход
          </Link>
        </div>
      </div>
    </div>
  );
}
