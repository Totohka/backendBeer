import { Button, Field, Input, Link, Stack } from "@chakra-ui/react";
import styles from "./sign-in.module.css";
import { PasswordInput } from "@/components/ui/password-input";

/* Фишки этого подхода:
- Все связанное с формой в одном месте и достаточно кратко
- ну типа инкапсуляция стилей и логики, дальше тупа штучки используешь эти и всо
- в каком-то смысле самодокументируемый код
- красиво и чисто в использовании
- ну что-то еще можно придумать определенно ахахха аля позволяет делать сложные формы и легко их изменять, меньше вложенности в файлах и отсутствует беготня по компачам и тд, мне лень думать еще ахах
*/

// ну я не особо внимательно сравнивал все формы, мб где-то стиль или еще чо проебал, главное суть передал

export const Form = ({ children }: { children: React.ReactNode }) => {
  return (
    <div className={styles["auth-page"]}>
      <div className={styles["container"]}>{children}</div>
    </div>
  );
};

// тут можно и пропс въебать, или отдельный сделать CompositionExampleForm.Title, в целом тут нет прям методологии по этому подходу
Form.InputsWrapper = ({
  children,
  title,
}: {
  title: string;
  children: React.ReactNode;
}) => {
  return (
    <div className={styles["container-inputs"]}>
      <Stack as="form" flex-direction="column" align="center">
        <h1 className={styles["header"]}>{title}</h1>
        {children}
      </Stack>
    </div>
  );
};

Form.Input = ({
  children,
  placeholder,
  isPassword,
}: {
  children: React.ReactNode;
  placeholder: string;
  isPassword?: boolean;
}) => {
  return (
    <Field.Root className={styles["input"]} invalid={false} required>
      <Field.Label>
        {children} <Field.RequiredIndicator />{" "}
      </Field.Label>
      {isPassword ? (
        <PasswordInput placeholder={placeholder} />
      ) : (
        <Input placeholder={placeholder} />
      )}
      <Field.ErrorText>Поле является обязательным</Field.ErrorText>
    </Field.Root>
  );
};

Form.SubmitButton = ({ children }: { children: React.ReactNode }) => {
  return (
    <Button colorPalette="blue" type="submit" className={styles["button"]}>
      {children}
    </Button>
  );
};

Form.Links = ({
  children,
}: {
  children: Array<{ href: string; text: string }>;
}) => {
  return (
    <div className={styles["container-links"]}>
      {children.map((link, index) => (
        <Link
          key={index}
          variant="plain"
          colorPalette="blue"
          href={link.href}
          className={index === children.length - 1 ? styles["last-link"] : ""}
        >
          {link.text}
        </Link>
      ))}
    </div>
  );
};

export const ExampleRegisterForm = () => (
  <Form>
    <Form.InputsWrapper title="Регистрация">
      <Form.Input placeholder="Введите ваше имя">Имя</Form.Input>
      <Form.Input placeholder="Введите вашу фамилию">Фамилия</Form.Input>
      <Form.Input placeholder="Введите ваше отчество">Отчество</Form.Input>
      <Form.Input placeholder="Введите ваш логин">Логин</Form.Input>
      <Form.Input placeholder="Введите ваш email">Email</Form.Input>
      <Form.Input placeholder="Введите ваш пароль" isPassword>
        Пароль
      </Form.Input>
      <Form.SubmitButton>Зарегистрироваться</Form.SubmitButton>
    </Form.InputsWrapper>
    <Form.Links>
      {[
        {
          href: "/signin",
          text: "Вход",
        },
        {
          href: "/signinfirst",
          text: "Первый вход",
        },
      ]}
    </Form.Links>
  </Form>
);
