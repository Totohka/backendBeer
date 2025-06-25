import { Button, Field, Input, Link, Stack } from "@chakra-ui/react";
import style from "./sign-in.module.css";
// FIXME: алиасы не настроены и нет index.ts, чтобы не писать каждый раз ../../../components/ui/password-input
import { PasswordInput } from "../../../components/ui/password-input";

export const SignIn = () => {
  // FIXME: какая-то большая вложенность, тут можно значительно меньше и тем самым упростить код и сделать его более читаемым
  return (
    <div className={style["auth-page"]}>
      <div className={style["container"]}>
        <div className={style["container-inputs"]}>
          {/* FIXME: для формы всегда стоит использовать компонент формы, а не просто div (семантика; внутренняя логика обработки формы, которая позволяет с ней работать через ее аргумент onSubmit и получать формДату) */}
          <Stack flex-direction="column" align="center">
            <h1 className={style["header"]}>Вход</h1>
            {/* FIXME: как-то неправильно что ипнут по дефолту невалидный invalid={true}*/}
            {/* FIXME: здесь везде используется одна и та же структура где меняется лишь placeholder и label, можно вынести в отдельный компонент*/}
            <Field.Root className={style["input"]} invalid={true} required>
              <Field.Label>
                Логин <Field.RequiredIndicator />{" "}
              </Field.Label>
              {/* FIXME: можно одинарным тегом, тип <Input/> */}
              <Input placeholder="Введите ваш логин"></Input>
              <Field.ErrorText>Поле является обязательным</Field.ErrorText>
            </Field.Root>
            {/* FIXME: как-то неправильно что ипнут по дефолту невалидный invalid={true}*/}
            <Field.Root className={style["input"]} invalid={true} required>
              <Field.Label>
                {/* FIXME: бесполезные пробел {" "} */}
                Пароль <Field.RequiredIndicator />{" "}
              </Field.Label>
              {/* FIXME: можно одинарным тегом, тип <PasswordInput/> */}
              <PasswordInput placeholder="Введите ваш пароль"></PasswordInput>
              <Field.ErrorText>Поле является обязательным</Field.ErrorText>
            </Field.Root>
            <Button colorPalette="blue" className={style["button"]}>
              Вход
            </Button>
          </Stack>
        </div>
        {/* FIXME: пустой div и причин не вижу, чтобы он был */}
        <div></div>
        <div className={style["container-links"]}>
          <Link
            variant="plain"
            colorPalette="blue"
            type="link"
            href="/register"
          >
            Регистрация
          </Link>
          {/* FIXME: можно с помощью :last-child сделать */}
          <Link
            className={style["last-link"]}
            variant="plain"
            colorPalette="blue"
            type="link"
            href="/signinfirst"
          >
            Первый вход
          </Link>
        </div>
      </div>
    </div>
  );
};
