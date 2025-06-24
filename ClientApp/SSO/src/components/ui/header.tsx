import { Button, Flex } from "@chakra-ui/react";
import style from './header.module.css';
import { useNavigate } from "react-router-dom";
export const Header = () => {
    const navigation = useNavigate();
    return (
        <div className={style["header"]}>
            <Flex colorPalette="" className={style["mainFlex"]} alignItems="center" justifyContent="space-between">
                <Flex justifyContent="space-around">
                    <Button onClick={() => {navigation("/profile")}} colorPalette="blue" className={style["button"]}>Мой профиль</Button>
                    <Button onClick={() => {navigation("/settings")}} colorPalette="blue" className={style["button"]}>Настройки</Button>
                    <Button onClick={() => {navigation("/admin")}} colorPalette="blue" className={style["button"]}>Администрирование</Button>
                </Flex>
                <Button colorPalette="red" className={style["button-out"]}>Выход</Button>
            </Flex>
        </div> 
    );
}