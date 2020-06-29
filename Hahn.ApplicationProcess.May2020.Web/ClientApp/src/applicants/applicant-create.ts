import { inject, reset, bindable } from 'aurelia-framework';
import { EventAggregator } from "aurelia-event-aggregator";

import { ApplicantCreated } from '../common/messages'
import { ApplicantService } from '../services/services';

import { Router } from 'aurelia-router';
import { Applicant } from '../models/applicant';

import { ValidationRules, ValidationControllerFactory } from 'aurelia-validation';
import { fromPairs } from 'lodash';

@inject(ValidationControllerFactory, EventAggregator, ApplicantService, Router)
export class ApplicantCreate {
  private _applicantService;
  private _ea;
  @bindable appl;
  applicant: Applicant;
  router: Router;
  controller;

  constructor(ValidationControllerFactory, ea: EventAggregator, applicantService: ApplicantService, router: Router) {
    this._applicantService = applicantService;
    this._ea = ea;
    this.router = router;
    this.controller = ValidationControllerFactory.createForCurrentScope();
  }

  create() {
    let apt = JSON.parse(JSON.stringify(this.applicant));

    this._applicantService.createApplcant(this.applicant)
      .then(applicant => {
        this._ea.publish(new ApplicantCreated(applicant));
      }).catch(err => console.log(err));
    this.router.navigateToRoute('applicants');

  }

  reset() {
    this.applicant = null;
  }

  public bind(newValue, oldValue) {
    if (this.applicant) {
      ValidationRules
        .ensure((a: Applicant) => a.name)
        .required()
        .on(this.applicant)
    }

    this.controller.validate();
  }
}
