import { createListCollection, Flex, Stat } from '@chakra-ui/react';
import { Header } from './../../../components/ui/header';
import style from './Settings.module.css';
import cx from 'classnames';
import { Select } from './../../../components/ui/select';
import { StatUpdate } from './../../../components/ui/stat-update';

export const Settings = () => {
    const languages = createListCollection({
        items: [
            { label: "Русский", value: "ru" },
            { label: "Английский", value: "en" }
        ]
    })
    
    return (
        <>
            <Header/>
            <div className={style["profile"]}>
                <Flex justifyContent="space-between">
                    <Stat.Root className={cx(style["fio-item"], style["stat-item"])}>
                        <StatUpdate label="Фамилия" value="Патюков"/>
                        <StatUpdate label="Имя" value="Дмитрий"/>
                        <StatUpdate label="Отчество" value="Анатольевич"/>
                    </Stat.Root>
                    <Stat.Root className={cx(style["fio-item"], style["stat-item"])}>
                        <StatUpdate label="Электронная почта" value="dimap190603@gmail.com"/>
                        <StatUpdate label="Логин" value="Totohka"/>
                    </Stat.Root>
                    <Stat.Root className={cx(style["fio-item"], style["stat-item"])}>
                        <Stat.Label>Язык</Stat.Label>
                        <Stat.ValueText><Select title="Выбор языка" placeholder="Русский" list={languages}></Select></Stat.ValueText>
                    </Stat.Root>
                </Flex>
            </div>
        </>
    )
}