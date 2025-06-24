import { Container, Link } from "@chakra-ui/react";
import { BsFillPostcardFill } from "react-icons/bs";
import { CiViewList } from "react-icons/ci";
import { FaLaptopCode } from "react-icons/fa";
import { FaPerson } from "react-icons/fa6";
import { GiHouseKeys } from "react-icons/gi";
import { MdGeneratingTokens } from "react-icons/md";
import { RiFileUserFill } from "react-icons/ri";
import style from "./NavigationMenu.module.css";
import { useNavigate } from "react-router-dom";

export const NavigationMenu = () => {
    const navigate = useNavigate();
    return (
        <div className={style["main"]}>
            <Container>
                <Link onClick={() => {navigate("/admin?model=ApiKeys")}}> <GiHouseKeys/> ApiKeys</Link>
            </Container>
            <Container>
                <Link onClick={() => {navigate("/admin?model=Applications")}}><FaLaptopCode/>Applications</Link>
            </Container>
            <Container>
                <Link onClick={() => {navigate("/admin?model=RefreshTokens")}}><MdGeneratingTokens/>RefreshTokens</Link>
            </Container>
            <Container>
                <Link onClick={() => {navigate("/admin?model=Roles")}}><BsFillPostcardFill/>Roles</Link>
            </Container>
            <Container>
                <Link onClick={() => {navigate("/admin?model=UserRoles")}}><RiFileUserFill/>UserRoles</Link>
            </Container>
            <Container>
                <Link onClick={() => {navigate("/admin?model=Users")}}><FaPerson/>Users</Link>
            </Container>
            <Container>
                <Link onClick={() => {navigate("/admin?model=WhiteList")}}><CiViewList/> WhiteList</Link>
            </Container>
        </div>
    );
} 