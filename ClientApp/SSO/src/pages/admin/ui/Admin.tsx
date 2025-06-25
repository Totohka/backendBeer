import { Flex } from "@chakra-ui/react";
import { Header } from "./../../../components/ui/header";
import { NavigationMenu } from "./NavigationMenu/NavigationMenu";
import { MainPart } from "./MainPart/MainPart";

export const Admin = () => {
  return (
    <>
      {/* FIXME: элемент не в общем layout для внутренних страниц */}
      <Header />
      <Flex>
        <NavigationMenu />
        <MainPart />
      </Flex>
    </>
  );
};
