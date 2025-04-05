import ProjectList from '../components/ProjectList';
import WelcomeBand from '../components/WelcomeBand';
import CartSummary from '../components/CartSummary';

function ProjectsPage() {
  return (
    <div className="container mt-4">
      <CartSummary />
      <WelcomeBand />
      <br />
      <div className="row">
        <div className="col-md-9">
          <ProjectList />
        </div>
      </div>
    </div>
  );
}

export default ProjectsPage;
