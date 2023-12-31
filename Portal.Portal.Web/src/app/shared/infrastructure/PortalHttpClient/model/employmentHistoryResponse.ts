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
import { EmployeeFileModel } from './employeeFileModel';
import { EmployeeRolesModel } from './employeeRolesModel';
import { FormerPositionModel } from './formerPositionModel';

export interface EmploymentHistoryResponse { 
    id?: number;
    contractType?: string;
    jobStartDate?: Date;
    contract?: EmployeeFileModel;
    contractExpirationDate?: Date;
    supervisor?: string;
    supervisorName?: string;
    formerPositions?: Array<FormerPositionModel>;
    roles?: Array<EmployeeRolesModel>;
}