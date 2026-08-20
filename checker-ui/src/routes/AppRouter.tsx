import { BrowserRouter, Routes, Route } from "react-router-dom";
import SpecificationPage from "../pages/SpecificationPage";
import SessionsPage from "../pages/SessionsPage";
import CreateSpecificationPage from "../pages/CreateSpecificationPage";
import EditSpecificationPage from "../pages/EditSpecificationPage";
import StatusPage from "../pages/StatusPage";
import OperatorPage from "../pages/OperatorPage";

function AppRouter(){
    return(
        <BrowserRouter>
            <Routes>
                
                <Route path="/" element={<StatusPage />} />

                <Route path="/operator" element={<OperatorPage />} />

                <Route path="/specifications" element={<SpecificationPage />} />

                <Route path="/sessions" element={<SessionsPage />} />

                <Route path="/specifications/create" element={<CreateSpecificationPage />} />

                <Route path="/specifications/edit/:id" element={<EditSpecificationPage />} />
            </Routes>
        </BrowserRouter>
    );
}

export default AppRouter;