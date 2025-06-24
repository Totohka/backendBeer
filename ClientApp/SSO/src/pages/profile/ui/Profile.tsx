import { Flex, Grid, Icon, Popover, Portal, Stat, Table } from '@chakra-ui/react';
import { Header } from './../../../components/ui/header';
import style from './Profile.module.css';
import cx from 'classnames';
import { CiCircleInfo } from 'react-icons/ci';
import { useState } from 'react';
import { PopoverIcon } from './../../../components/ui/popover-icon';

export const Profile = () => {
    const [open, setOpen] = useState(false);
    return (
        <>
            <Header/>
            <div className={style["profile"]}>
                <Flex justifyContent="space-between">
                    <Stat.Root className={cx(style["fio-item"], style["stat-item"])}>
                        <Stat.Label>Фамилия</Stat.Label>
                        <Stat.ValueText>Патюков</Stat.ValueText>
                        <Stat.Label>Имя</Stat.Label>
                        <Stat.ValueText>Дмитрий</Stat.ValueText>
                        <Stat.Label>Отчество</Stat.Label>
                        <Stat.ValueText>Анатольевич</Stat.ValueText>
                    </Stat.Root>
                    <Stat.Root className={cx(style["fio-item"], style["stat-item"])}>
                        <Stat.Label>Электронная почта</Stat.Label>
                        <Stat.ValueText>dimap190603@gmail.com</Stat.ValueText>
                        <Stat.Label>Логин</Stat.Label>
                        <Stat.ValueText>Totohka</Stat.ValueText>
                    </Stat.Root>
                </Flex>
                <Flex>
                    <Stat.Root className={cx(style["fio-item"], style["stat-item"])}>
                        <Stat.Label>Дата создания</Stat.Label>
                        <Stat.ValueText>2025-03-01 12:16:16.129127+07</Stat.ValueText>
                        <Stat.Label>Дата последнего обновления</Stat.Label>
                        <Stat.ValueText>2025-03-01 12:16:16.129127+07</Stat.ValueText>
                    </Stat.Root>
                    <Stat.Root className={cx(style["fio-item"], style["stat-item"])}>
                        <Stat.Label>
                            Авторизация по API-ключу
                            <PopoverIcon 
                                text="В будущем будет возможно получение персонального API-ключа" 
                                icon={<CiCircleInfo />}>
                            </PopoverIcon> 
                        </Stat.Label>

                        <Stat.ValueText>Доступна</Stat.ValueText>
                        <Stat.Label>Авторизация по паролю</Stat.Label>
                        <Stat.ValueText>Доступна</Stat.ValueText>
                    </Stat.Root>
                    <Stat.Root className={cx(style["fio-item"], style["stat-item"])}>
                        <Stat.Label>
                            UID
                            <PopoverIcon 
                                text="Ваш уникальный идентификатор" 
                                icon={<CiCircleInfo />}>
                            </PopoverIcon> 
                        </Stat.Label>
                        <Stat.ValueText>7a1bda3f-9543-4208-aacd-2dd6d07a5d5a</Stat.ValueText>
                        <Stat.Label>Язык</Stat.Label>
                        <Stat.ValueText>Русский</Stat.ValueText>
                    </Stat.Root>
                </Flex>
            </div>
        </>
    )
}