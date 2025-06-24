import { Button, Field, Input, Link, Stack } from "@chakra-ui/react";
import style from "./sign-in.module.css";
import { PasswordInput } from "../../../components/ui/password-input";

export const SignIn = () => {
    return(
        <div className={style["auth-page"]}>
            <div className={style["container"]}>
                <div className={style["container-inputs"]}>
                    <Stack flex-direction="column" align="center">
                        <h1 className={style["header"]}>Вход</h1>
                        <Field.Root className={style["input"]} invalid={true} required>
                            <Field.Label>Логин <Field.RequiredIndicator /> </Field.Label>
                            <Input placeholder="Введите ваш логин"></Input> 
                            <Field.ErrorText>Поле является обязательным</Field.ErrorText>
                        </Field.Root>
                        <Field.Root className={style["input"]} invalid={true} required>
                            <Field.Label>Пароль <Field.RequiredIndicator /> </Field.Label>
                            <PasswordInput placeholder="Введите ваш пароль"></PasswordInput>
                            <Field.ErrorText>Поле является обязательным</Field.ErrorText>
                        </Field.Root>
                        <Button colorPalette="blue" className={style["button"]}>Вход</Button>
                    </Stack>
                </div>
                <div>
                </div>
                <div className={style["container-links"]}>
                    <Link variant="plain" colorPalette="blue" type="link" href="/register">Регистрация</Link>
                    <Link className={style["last-link"]} variant="plain" colorPalette="blue" type="link" href="/signinfirst">Первый вход</Link>
                </div>
            </div>
        </div>
    );
}