/**
 * Demo API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { Conviction } from './conviction';
import { Gender } from './gender';
import { MaritalStatus } from './maritalStatus';

export interface EditOtherInformationRequest { 
    employeeId?: number;
    gender?: Gender;
    maritalStatus?: MaritalStatus;
    legalAddress?: string;
    actualAddress?: string;
    conviction?: Conviction;
    fullName?: string;
    relationship?: string;
    phoneNumber?: string;
}