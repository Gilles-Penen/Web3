
import Header from "../component/Header";

const RootLayout = () => {
  return (
    <div className="flex flex-col min-h-screen">
      <Header />
      <div className="flex flex-grow">
        <Outlet />
      </div>
    </div>
  );
};

export default RootLayout;
