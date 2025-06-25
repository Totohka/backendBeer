import { Button, Flex } from "@chakra-ui/react";
import style from "./header.module.css";
import { useNavigate } from "react-router-dom";
export const Header = () => {
  const navigation = useNavigate();
  return (
    // FIXME: лучше использовать тег header, а не div семантика...
    <div className={style["header"]}>
      <Flex
        colorPalette=""
        className={style["mainFlex"]}
        alignItems="center"
        justifyContent="space-between"
      >
        <Flex justifyContent="space-around">
          {/* FIXME: всегда лучше использовать <Link> для ссылок/перемещений и стилизовать ее правильно, чтобы была семантика и все там остальное хорошее
          В чакре легко можно сделать так (внимание на asChild):
          <Button asChild>
            <Link>Мой профиль</Link>
          </Button>

          ссылки еще и могут на сервере рендериться, в отличии от хуков
          */}

          <Button
            onClick={() => {
              navigation("/profile");
            }}
            colorPalette="blue"
            className={style["button"]}
          >
            Мой профиль
          </Button>
          <Button
            onClick={() => {
              navigation("/settings");
            }}
            colorPalette="blue"
            className={style["button"]}
          >
            Настройки
          </Button>
          <Button
            onClick={() => {
              navigation("/admin");
            }}
            colorPalette="blue"
            className={style["button"]}
          >
            Администрирование
          </Button>
        </Flex>
        <Button colorPalette="red" className={style["button-out"]}>
          Выход
        </Button>
      </Flex>
    </div>
  );
};
