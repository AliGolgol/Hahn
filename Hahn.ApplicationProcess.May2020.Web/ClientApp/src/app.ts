import { Aurelia, PLATFORM } from 'aurelia-framework';
import { Router, RouterConfiguration } from 'aurelia-router';

export class App {
  router: Router | undefined;

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Hahn Applicant Process';
    config.map([{
      route: [ '', 'home' ],
      name: 'home',
      moduleId: PLATFORM.moduleName('./home/home'),
      title: 'Home'
    }, {
      route: 'applicants-list',
      name: 'applicants',
      moduleId: PLATFORM.moduleName('./applicants/applicant-list'),
      title: 'Applicant List'
    }, {
        route: 'applicant-update/:d',
        name: 'ApplicantUpdate',
        moduleId: PLATFORM.moduleName('./applicants/applicant-update')
      }, {
        route: 'applicants/create',
        name: 'ApplicantCreate',
        moduleId: PLATFORM.moduleName('./applicants/applicant-create')
      }, {
        route: 'applicants/detail/:id',
        name: 'ApplicantDetail',
        moduleId: PLATFORM.moduleName('./applicants/applicant-detail')
      }
    ]);

    this.router = router;
  }
}
