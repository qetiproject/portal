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
import { NonComplianceContentModel } from './nonComplianceContentModel';
import { NonComplianceStatus } from './nonComplianceStatus';

export interface SentNonCompliancesModel { 
    number?: string;
    group?: string;
    status?: NonComplianceStatus;
    date?: Date;
    violator?: string;
    content?: NonComplianceContentModel;
}