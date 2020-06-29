
export interface Applicant {
  id:number,
  name: string;
  familyName: string;
  address: string;
  countryOfOrigin: string;
  emailAddress: string;
  age: number;
  hired: boolean;
}
// ValidationRules
//   .ensure('emailAddress').required()
//   .on(Applicant);
